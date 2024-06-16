using ApiSharedClasses.QueryParameters;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Users;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Repository
{
	public class RoleRepository : RepositoryBase<Role>, IRoleRepository
	{
		public RoleRepository(NotkaDatabaseContext repositoryContext) 
			: base(repositoryContext)
		{
		}

		public async Task<PagedList<RoleForView>> GetRoles(int userId, RoleParameters roleParameters)
		{
			//sprawdzenie, czy role ma odpowiednią rolę
			var rolesForView = Context.Role.Select(role => ModelConverters.ConvertToRoleForView(role));
			return await PagedList<RoleForView>.CreateAsync(rolesForView, 1, 10);
		}

		public async Task<RoleForView> GetRoleById(int userId, int id)
		{
			var role = await Context.Role.SingleOrDefaultAsync(r => r.Id == id);

			return ModelConverters.ConvertToRoleForView(role);
		}

		public async Task<RoleForView> CreateRole(RoleForView role)
		{
			var roleToAdd = new Role().CopyProperties(role);

			Context.Role.Add(roleToAdd);
			await Context.SaveChangesAsync();

			return ModelConverters.ConvertToRoleForView(roleToAdd);
		}

		public async Task UpdateRole(int id, RoleForView role)
		{
			var roleToAdd = new Role().CopyProperties(role);
			Context.Entry(roleToAdd).State = EntityState.Modified;

			try
			{
				await Context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!RoleExists(id))
				{
					throw new NotFoundException();
				}
				else
				{
					throw;
				}
			}
		}

		public async Task DeleteRole(int userId, int id)
		{
			var role = await Context.Role.SingleOrDefaultAsync(tag => tag.Id == id);

			if (role == null)
			{
				throw new NotFoundException();
			}
			//FIXME user powinien mieć uprawnienia

			Delete(role);
			await Context.SaveChangesAsync();
		}

		private bool RoleExists(int id)
		{
			return Context.Role.Any(e => e.Id == id);
		}
	}
}
