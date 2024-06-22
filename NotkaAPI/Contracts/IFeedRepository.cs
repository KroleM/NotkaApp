using ApiSharedClasses.QueryParameters;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Contracts
{
	public interface IFeedRepository
	{
		Task<PagedList<FeedForView>> GetFeeds(int userId, FeedParameters feedParameters);
		Task<FeedForView> GetFeedById(int userId, int id);
		Task<FeedForView> CreateFeed(int userId, FeedForView feed);
		Task UpdateFeed(int id, FeedForView feed);
		Task DeleteFeed(int userId, int id);
	}
}
