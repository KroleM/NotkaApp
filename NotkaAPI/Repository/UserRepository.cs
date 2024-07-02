using ApiSharedClasses.QueryParameters;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Investments;
using NotkaAPI.Models.Notes;
using NotkaAPI.Models.Users;
using NotkaAPI.ViewModels;
using System.Diagnostics;
using System.Security.Policy;

namespace NotkaAPI.Repository
{
	public class UserRepository : RepositoryBase<User>, IUserRepository
	{
		public UserRepository(NotkaDatabaseContext repositoryContext) 
			: base(repositoryContext)
		{
		}

		public async Task<PagedList<UserForView>> GetUsers(int userId, UserParameters userParameters)
		{
			//Check if user is Admin
			if (!Context.User.Any(u  => u.Id == userId && u.RoleUsers.Any(ru => ru.RoleId == 3)))
			{
				throw new UnauthorizedException();
			}

			var usersForView = Context.User.Select(user => ModelConverters.ConvertToUserForView(user));

			return await PagedList<UserForView>.CreateAsync(usersForView, 1, 10);
		}
		public async Task<UserForView> GetUserById(int userId, int id)
		{
			//weryfikacja roli usera pytającego
			if (!await Context.User.AnyAsync(u => u.Id == id))
			{
				throw new NotFoundException();
			}
			var user = await Context.User
						.Include(user => user.RoleUsers)
						.ThenInclude(roleuser => roleuser.Role)
						.SingleOrDefaultAsync(user => user.Id == id);

			return ModelConverters.ConvertToUserForView(user);
		}
		public async Task<UserForView> GetUserWithAuth(string email, string hash)
		{
			var user = await Context.User
				.Include(user => user.RoleUsers)
				.ThenInclude(roleuser => roleuser.Role)
				.SingleOrDefaultAsync(u => u.Email == email);

			if (user == null)
			{
				throw new NotFoundException();
			}
			if (hash != user.PasswordHash)
			{
				throw new NotFoundException();
			}

			return ModelConverters.ConvertToUserForView(user);
		}
		public async Task<UserForView> CreateUser(UserForView user)
		{
			if (await Context.User.AnyAsync(u => u.Email == user.Email))
			{
				throw new ConflictException();
			}

			var userToAdd = new User().CopyProperties(user);
			Create(userToAdd);  // = Context.User.Add(userToAdd);
			await Context.SaveChangesAsync();   //assigns Id in the DB
			//give role
			Context.RoleUser.Add(new RoleUser
			{
				Id = 0,
				IsActive = true,
				CreatedDate = DateTimeOffset.Now,
				ModifiedDate = DateTimeOffset.Now,
				UserId = userToAdd.Id,
				RoleId = 4,
			});
			//create portfolio
			var newPortfolio = new Portfolio
			{
				Id = 0,
				IsActive = true,
				CreatedDate = DateTimeOffset.Now,
				ModifiedDate = DateTimeOffset.Now,
				UserId = userToAdd.Id,
				Name = (string.IsNullOrWhiteSpace(user.FirstName) ? user.Email : user.FirstName) + "_portfolio",
			};
			Context.Portfolio.Add(newPortfolio);
			await Context.SaveChangesAsync();

			var addedUser = await Context.User
					.Include(user => user.RoleUsers)
					.ThenInclude(roleuser => roleuser.Role)
					.SingleOrDefaultAsync(user => user.Id == userToAdd.Id);

			return ModelConverters.ConvertToUserForView(addedUser);
		}
		public async Task UpdateUser(int id, UserForView user)
		{
			var userToAdd = new User().CopyProperties(user);
			Context.Entry(userToAdd).State = EntityState.Modified;

			try
			{
				using (var dbContextTransaction = Context.Database.BeginTransaction())
				{
					//RoleUser
					Context.RoleUser.RemoveRange(await Context.RoleUser.Where(ru => ru.UserId == userToAdd.Id).ToArrayAsync());
					if (!user.RolesForView.IsNullOrEmpty())
					{
						foreach (var roleForView in user.RolesForView)
						{
							await AddToContextRoleUserAsync(roleForView, userToAdd.Id);
						}
					}
					await Context.SaveChangesAsync();
					dbContextTransaction.Commit();
				}
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UserExists(id))
				{
					throw new NotFoundException();
				}
				else
				{
					throw;
				}
			}
		}
		public async Task DeleteUser(int userId, int id)
		{
			var user = await Context.User.SingleOrDefaultAsync(user => user.Id == id);

			if (user == null)
			{
				throw new NotFoundException();
			}
			//FIXME user powinien mieć uprawnienia
			if (user.Id != userId)
			{
				throw new ForbidException();
			}

			Delete(user);
			await Context.SaveChangesAsync();
		}
		private bool UserExists(int id)
		{
			return Context.User.Any(e => e.Id == id);
		}
		private async Task AddToContextRoleUserAsync(RoleForView roleForView, int userId)
		{
			var role = new Role().CopyProperties(roleForView);
			//if (role.Id == 0)
			//{
			//	Context.Role.Add(role);
			//	await Context.SaveChangesAsync();
			//}
			Context.RoleUser.Add(new RoleUser
			{
				Id = 0,
				IsActive = true,
				UserId = userId,
				RoleId = role.Id,
			});
		}
	}
}
