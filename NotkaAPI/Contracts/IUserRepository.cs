using ApiSharedClasses.QueryParameters;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Contracts
{
	public interface IUserRepository
	{
		Task<PagedList<UserForView>> GetUsers(int userId, UserParameters userParameters);
		Task<UserForView> GetUserWithAuth(string email, string hash);
		Task<UserForView> CreateUser(UserForView user);
		Task UpdateUser(int id, UserForView user);
		Task DeleteUser(int userId, int id);
	}
}
