using ApiSharedClasses.QueryParameters;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Contracts
{
	public interface ITagRepository //: IRepositoryBase<TagForView>
	{
		Task<PagedList<TagForView>> GetTags(int userId, TagParameters tagParameters);
		Task<TagForView> GetTagById(int userId, int id);
		Task<TagForView> CreateTag(TagForView tag);
		Task UpdateTag(int id, TagForView tag);
		Task DeleteTag(int userId, int id);
	}
}
