using ApiSharedClasses.QueryParameters;
using ApiSharedClasses.SortValues;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Notes;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Repository
{
	public class ListRepository : RepositoryBase<List>, IListRepository
	{
		public ListRepository(NotkaDatabaseContext repositoryContext)
			: base(repositoryContext)
		{
		}
		public async Task<PagedList<ListForView>> GetLists(int userId, ListParameters listParameters)
		{
			if (Context.List.Where(n => n.UserId == userId) == null)
			{
				throw new NotFoundException();
			}

			var lists = FindByCondition(n => n.UserId == userId);

			SearchByPhrase(ref lists, listParameters.SearchPhrase);

			ApplySort(ref lists, listParameters.SortOrder);

			var listsWithIncludes = lists.Include(list => list.ListTags.OrderBy(lt => lt.Tag.Name)).ThenInclude(listtag => listtag.Tag)
										 .Include(list => list.ListElements);

			return await PagedList<ListForView>.CreateAsync(listsWithIncludes
						.Select(list => ModelConverters.ConvertToListForView(list)),
							listParameters.PageNumber,
							listParameters.PageSize);
		}
		public async Task<ListForView> GetListById(int userId, int id)
		{
			if (!await Context.List.AnyAsync(l => l.Id == id))
			{
				throw new NotFoundException();
			}
			var list = await Context.List
				.Include(list => list.ListTags)
				.ThenInclude(listtag => listtag.Tag)
				.Include(list => list.ListElements)	//FIXME czy odwołanie w ListElements do List nie prowadzi do duplikowania danych?
				.SingleOrDefaultAsync(list => list.Id == id);
			if (list.UserId != userId)
			{
				throw new UnauthorizedException();
			}

			return ModelConverters.ConvertToListForView(list);
		}
		public async Task<ListForView> CreateList(ListForView list)
		{
			var listToAdd = new List().CopyProperties(list);

			using (var dbContextTransaction = Context.Database.BeginTransaction())
			{
				//This goes first, so that later listToAdd.Id can be extracted
				Create(listToAdd);
				await Context.SaveChangesAsync();

				if (!list.TagsForView.IsNullOrEmpty())
				{
					foreach (var tagForView in list.TagsForView)
					{
						await AddToContextListTagAsync(tagForView, listToAdd.Id);
						await Context.SaveChangesAsync();
					}
				}
				if (!list.ListElementsForView.IsNullOrEmpty())
				{ 
					foreach (var listElementForView in list.ListElementsForView)
					{
						await AddToContextListElementAsync(listElementForView, listToAdd.Id);
						await Context.SaveChangesAsync();
					}
				}
				dbContextTransaction.Commit();
			}

			var uploadedList = await Context.List
				.Include(list => list.ListTags)
				.ThenInclude(listtag => listtag.Tag)
				.Include(list => list.ListElements) //FIXME czy odwołanie w ListElements do List nie prowadzi do duplikowania danych?
				.SingleOrDefaultAsync(list => list.Id == listToAdd.Id);
			//await _context.Entry(uploadedList).ReloadAsync();
			return ModelConverters.ConvertToListForView(uploadedList);
		}
		public async Task UpdateList(int id, ListForView list)
		{
			var listToAdd = new List().CopyProperties(list);
			Context.Entry(listToAdd).State = EntityState.Modified;

			try
			{
				using (var dbContextTransaction = Context.Database.BeginTransaction())
				{
					//ListTags
					Context.ListTag.RemoveRange(await Context.ListTag.Where(lt => lt.ListId == listToAdd.Id).ToArrayAsync());
					if (!list.TagsForView.IsNullOrEmpty())
					{
						foreach (var tagForView in list.TagsForView)
						{
							await AddToContextListTagAsync(tagForView, listToAdd.Id);
						}
					}
					//ListElements
					Context.ListElement.RemoveRange(await Context.ListElement.Where(le => le.ListId == listToAdd.Id).ToArrayAsync());
					if (!list.ListElementsForView.IsNullOrEmpty())
					{
						foreach (var listElementForView in list.ListElementsForView)
						{
							await AddToContextListElementAsync(listElementForView, listToAdd.Id);
						}
					}

					await Context.SaveChangesAsync();
					dbContextTransaction.Commit();
				}
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ListExists(id))
				{
					throw new NotFoundException();
				}
				else
				{
					throw;
				}
			}
		}
		public async Task DeleteList(int userId, int id)
		{
			var list = await Context.List.FindAsync(id);   //When list is deleted, ListElements are deleted too ??
			if (list == null)
			{
				throw new NotFoundException();
			}
			if (list.UserId != userId)
			{
				throw new ForbidException();
			}

			Delete(list);
			await Context.SaveChangesAsync();
		}
		private bool ListExists(int id)
		{
			return (Context.List?.Any(e => e.Id == id)).GetValueOrDefault();
		}
		private async Task AddToContextListTagAsync(TagForView tagForView, int listId)
		{
			var tag = new Tag().CopyProperties(tagForView);
			if (tag.Id == 0)
			{
				Context.Tag.Add(tag);
				await Context.SaveChangesAsync();
			}
			Context.ListTag.Add(new ListTag
			{
				Id = 0,
				IsActive = true,
				ListId = listId,
				TagId = tag.Id,
			});
		}
		private async Task AddToContextListElementAsync(ListElementForView listElementForView, int listId)
		{
			var listElement = new ListElement().CopyProperties(listElementForView);
			listElement.ListId = listId;
			if (listElement.Id == 0)
			{
				Context.ListElement.Add(listElement);
				await Context.SaveChangesAsync();
			}
		}
		private void SearchByPhrase(ref IQueryable<List> lists, string? searchPhrase)
		{
			if (!lists.Any() || string.IsNullOrWhiteSpace(searchPhrase))
				return;
			lists = lists.Where(l => l.Name.ToLower().Contains(searchPhrase.Trim().ToLower()));
		}
		private void ApplySort(ref IQueryable<List> lists, string? orderByString)
		{
			if (!lists.Any())
				return;

			NoteSortValue noteSortEnum = new();

			if (Enum.TryParse(orderByString, out noteSortEnum))
			{
				switch (noteSortEnum)
				{
					case NoteSortValue.FromAtoZ:
						lists = lists.OrderBy(x => x.Name).ThenByDescending(x => x.ModifiedDate);
						break;
					case NoteSortValue.FromZtoA:
						lists = lists.OrderByDescending(x => x.Name).ThenByDescending(x => x.ModifiedDate);
						break;
					case NoteSortValue.ByCreationDateAscending:
						lists = lists.OrderBy(x => x.CreatedDate).ThenByDescending(x => x.ModifiedDate);
						break;
					case NoteSortValue.ByCreationDateDescending:
						lists = lists.OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.ModifiedDate);
						break;
					case NoteSortValue.ByModificationDateAscending:
						lists = lists.OrderBy(x => x.ModifiedDate);
						break;
					case NoteSortValue.ByModificationDateDescending:
						lists = lists.OrderByDescending(x => x.ModifiedDate);
						break;
				}
			}
			else
			{
				lists = lists.OrderByDescending(x => x.ModifiedDate);
			}
		}
	}
}
