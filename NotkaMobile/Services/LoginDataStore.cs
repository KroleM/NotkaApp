using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;

namespace NotkaMobile.Services
{
	public class LoginDataStore : ADataStore
    {
		private User _user = new();
		public async Task<User> LoginUser(string email, string passwordHash)
		{
			_user = await _service.GetUserWithAuthAsync(email, passwordHash);
			
			return _user;
		}

	}
}
