using ApiSharedClasses.QueryParameters;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Notes;
using NotkaAPI.ViewModels;
using static Azure.Core.HttpHeader;

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
		public Task<TagForView> GetTagById(int userId, int id)
		{
			throw new NotImplementedException();
		}
		public Task<TagForView> CreateTag(TagForView tag)
		{
			throw new NotImplementedException();
		}

		public Task DeleteTag(int userId, int id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateTag(int id, TagForView tag)
		{
			throw new NotImplementedException();
		}
	}
}
