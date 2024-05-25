using ApiSharedClasses.QueryParameters;
using Azure;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
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
			//FIXME pobranie userów zależnie od roli (uprawnień)
			throw new NotImplementedException();
		}
		public async Task<UserForView> GetUserWithAuth(string email, string hash)
		{
			var user = await Context.User.SingleOrDefaultAsync(u => u.Email == email);

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

			Context.User.Add(userToAdd);
			await Context.SaveChangesAsync();

			return ModelConverters.ConvertToUserForView(userToAdd);
		}
		public async Task UpdateUser(int id, UserForView user)
		{
			var userToAdd = new User().CopyProperties(user);
			Context.Entry(userToAdd).State = EntityState.Modified;

			try
			{
				await Context.SaveChangesAsync();
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
			var user = await Context.User
					.SingleOrDefaultAsync(tag => tag.Id == id);

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
	}
}
