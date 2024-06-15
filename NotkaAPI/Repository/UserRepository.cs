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
			//if (Context.User.Where(n => n.UserId == userId) == null)
			//{
			//	throw new NotFoundException();
			//}

			//var notes = FindByCondition(n => n.UserId == userId
			//							&& n.CreatedDate >= noteParameters.MinDateOfCreation
			//							&& n.CreatedDate <= noteParameters.MaxDateOfCreation
			//							&& ((noteParameters.HasPicture ?? false) ? n.Picture != null : ((noteParameters.HasPicture ?? true) ? (n.Picture != null || n.Picture == null) : n.Picture == null))
			//							);

			//SearchByPhrase(ref notes, noteParameters.SearchPhrase);

			//ApplySort(ref notes, noteParameters.SortOrder);

			//var notesWithIncludes = notes.Include(note => note.NoteTags.OrderBy(nt => nt.Tag.Name)).ThenInclude(notetag => notetag.Tag);

			//return await PagedList<NoteForView>.CreateAsync(notesWithIncludes
			//			.Select(note => ModelConverters.ConvertToNoteForView(note)),
			//				noteParameters.PageNumber,
			//				noteParameters.PageSize);

			var usersForView = Context.User.Select(user => ModelConverters.ConvertToUserForView(user));
			return await PagedList<UserForView>.CreateAsync(usersForView, 1, 10);
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
