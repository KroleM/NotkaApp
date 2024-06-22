using ApiSharedClasses.QueryParameters;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Notes;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Repository
{
	public class TagRepository : RepositoryBase<Tag>, ITagRepository
	{
		public TagRepository(NotkaDatabaseContext repositoryContext)
			: base(repositoryContext)
		{
		}
		public async Task<PagedList<TagForView>> GetTags(int userId, TagParameters tagParameters)
		{
			if (Context.Tag.Where(n => n.UserId == userId) == null)
			{
				throw new NotFoundException();
			}
			var tags = FindByCondition(n => n.UserId == userId);
										//&& n.CreatedDate >= noteParameters.MinDateOfCreation
										//&& n.CreatedDate <= noteParameters.MaxDateOfCreation)

			//TBD: Searching
			return await PagedList<TagForView>.CreateAsync(tags.OrderBy(t => t.Name)
							.Select(tag => ModelConverters.ConvertToTagForView(tag)),
										tagParameters.PageNumber,
										tagParameters.PageSize);
		}
		public async Task<TagForView> GetTagById(int userId, int id)
		{
			if (!await Context.Tag.AnyAsync(t => t.Id == id))
			{
				throw new NotFoundException();
			}
			var tag = await Context.Tag
				.Include(tag => tag.NoteTags)
				.ThenInclude(notetag => notetag.Note)
				.ThenInclude(note => note.NoteTags)
				.ThenInclude(notetag => notetag.Tag)
				.SingleOrDefaultAsync(tag => tag.Id == id);
			if (tag.UserId != userId)
			{
				throw new UnauthorizedException();
			}

			return ModelConverters.ConvertToTagForView(tag);
		}
		public async Task<TagForView> CreateTag(TagForView tag)
		{
			var tagToAdd = new Tag().CopyProperties(tag);
			Context.Tag.Add(tagToAdd);
			await Context.SaveChangesAsync();

			return ModelConverters.ConvertToTagForView(tagToAdd);
		}
		public async Task UpdateTag(int id, TagForView tag)
		{
			var tagToAdd = new Tag().CopyProperties(tag);
			Context.Entry(tagToAdd).State = EntityState.Modified;

			try
			{
				await Context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TagExists(id))
				{
					throw new NotFoundException();
				}
				else
				{
					throw;
				}
			}
		}
		public async Task DeleteTag(int userId, int id)
		{
			var tag = await Context.Tag
				.Include(tag => tag.NoteTags)
				.SingleOrDefaultAsync(tag => tag.Id == id);

			if (tag == null)
			{
				throw new NotFoundException();
			}
			if (tag.UserId != userId)
			{
				throw new ForbidException();
			}

			Delete(tag);
			await Context.SaveChangesAsync();
		}
		private bool TagExists(int id)
		{
			return Context.Tag.Any(e => e.Id == id);
		}
	}
}
