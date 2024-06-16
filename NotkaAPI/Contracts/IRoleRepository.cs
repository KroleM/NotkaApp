using ApiSharedClasses.QueryParameters;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Contracts
{
	public interface IRoleRepository
	{
		Task<PagedList<RoleForView>> GetRoles(int userId, RoleParameters roleParameters);
		Task<RoleForView> GetRoleById(int userId, int id);
		Task<RoleForView> CreateRole(RoleForView role);
		Task UpdateRole(int id, RoleForView role);
		Task DeleteRole(int userId, int id);
	}
}
