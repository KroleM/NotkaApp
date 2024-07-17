using ApiSharedClasses.QueryParameters;
using ApiSharedClasses.SortValues;
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

			var users = Context.User.AsQueryable();

			SearchByPhrase(ref users, userParameters.SearchPhrase);

			ApplySort(ref users, userParameters.SortOrder);

			var usersWithIncludes = users.Where(u => ((userParameters.IsActive ? u.IsActive == true : (u.IsActive == true || u.IsActive == false))))
							.Include(u => u.RoleUsers)
							.ThenInclude(ru => ru.Role)
							.Where(u => u.RoleUsers.Any(ru => userParameters.RoleId == 0 ? true : ru.RoleId == userParameters.RoleId));

			return await PagedList<UserForView>.CreateAsync(usersWithIncludes.Select(user => ModelConverters.ConvertToUserForView(user)),
										userParameters.PageNumber,
										userParameters.PageSize);
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
		public async Task<UserForView> GetUserWithAuth(string email, string password)
		{
			var user = await Context.User
				.Include(user => user.RoleUsers)
				.ThenInclude(roleuser => roleuser.Role)
				.SingleOrDefaultAsync(u => u.Email == email);

			if (user == null)
			{
				throw new NotFoundException();
			}
			if (!PasswordHelper.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
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

			byte[] passwordSalt;
			userToAdd.PasswordHash = PasswordHelper.HashPassword(user.Password, out passwordSalt);
			userToAdd.PasswordSalt = passwordSalt;

			Create(userToAdd);  // = Context.User.Add(userToEdit);
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
			var userToEdit = await Context.User.SingleOrDefaultAsync(user => user.Id == id);

			if (user == null)
			{
				throw new NotFoundException();
			}
			userToEdit.CopyProperties(user);
			if (!string.IsNullOrWhiteSpace(user.Password))
			{
				byte[] passwordSalt;
				userToEdit.PasswordHash = PasswordHelper.HashPassword(user.Password, out passwordSalt);
				userToEdit.PasswordSalt = passwordSalt;
			}

			Context.Entry(userToEdit).State = EntityState.Modified;

			try
			{
				using (var dbContextTransaction = Context.Database.BeginTransaction())
				{
					//RoleUser
					Context.RoleUser.RemoveRange(await Context.RoleUser.Where(ru => ru.UserId == userToEdit.Id).ToArrayAsync());
					if (!user.RolesForView.IsNullOrEmpty())
					{
						foreach (var roleForView in user.RolesForView)
						{
							await AddToContextRoleUserAsync(roleForView, userToEdit.Id);
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
		private void SearchByPhrase(ref IQueryable<User> users, string? searchPhrase)
		{
			if (!users.Any() || string.IsNullOrWhiteSpace(searchPhrase))
				return;
			users = users.Where(n => n.Email.ToLower().Contains(searchPhrase.Trim().ToLower())
						|| n.LastName.ToLower().Contains(searchPhrase.Trim().ToLower()));
		}
		private void ApplySort(ref IQueryable<User> users, string? orderByString)
		{
			if (!users.Any())
				return;

			UserSortValue userSortEnum = new();

			if (Enum.TryParse(orderByString, out userSortEnum))
			{
				switch (userSortEnum)
				{
					case UserSortValue.EmailFromAtoZ:
						users = users.OrderBy(x => x.Email).ThenBy(x => x.LastName);
						break;
					case UserSortValue.EmailFromZtoA:
						users = users.OrderByDescending(x => x.Email).ThenByDescending(x => x.LastName);
						break;
					case UserSortValue.FirstNameFromAtoZ:
						users = users.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
						break;
					case UserSortValue.FirstNameFromZtoA:
						users = users.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName);
						break;
					case UserSortValue.LastNameFromAtoZ:
						users = users.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
						break;
					case UserSortValue.LastNameFromZtoA:
						users = users.OrderByDescending(x => x.LastName).ThenByDescending(x => x.FirstName);
						break;
				}
			}
			else
			{
				users = users.OrderByDescending(x => x.ModifiedDate);
			}
		}
	}
}
