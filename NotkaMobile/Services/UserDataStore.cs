using ApiSharedClasses.QueryParameters;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;

namespace NotkaMobile.Services
{
	public class UserDataStore : ADataStore
    {
		public UserParameters Params { get; set; }
		public UserDataStore() 
		{ 
			Params = new UserParameters();
		}
		public async Task<UserForView> LoginUser(string email, string passwordHash)
		{
			return await _service.UserGETWithAuthAsync(email, passwordHash);
		}

	}
}
