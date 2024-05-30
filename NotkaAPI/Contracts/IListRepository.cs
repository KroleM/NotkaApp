using ApiSharedClasses.QueryParameters;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Contracts
{
	public interface IListRepository
	{
		Task<PagedList<ListForView>> GetLists(int userId, ListParameters listParameters);
		Task<ListForView> GetListById(int userId, int id);
		Task<ListForView> CreateList(ListForView list);
		Task UpdateList(int id, ListForView list);
		Task DeleteList(int userId, int id);
	}
}
