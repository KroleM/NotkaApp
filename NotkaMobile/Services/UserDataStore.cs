using ApiSharedClasses.QueryParameters;
using NotkaMobile.Helpers;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;

namespace NotkaMobile.Services
{
	public class UserDataStore : AListDataStore<UserForView, UserParameters>
	{
		public UserDataStore() 
		{ 
			Params = new UserParameters();
		}
		public async Task<UserForView> LoginUser(string email, string password)
		{
			return await _service.UserGETWithAuthAsync(email, password);
		}

		public override async Task<UserForView> AddItemToService(UserForView item)
		{
			return await _service.UserPOSTAsync(item);
		}

		public override async Task<bool> DeleteItemFromService(UserForView item)
		{
			return await _service.UserDELETEAsync(Preferences.Default.Get("userId", 0), item.Id).HandleRequest();
		}

		public override async Task<UserForView> Find(UserForView item)
		{
			throw new NotImplementedException();
		}

		public override async Task<UserForView> Find(int id)
		{
			throw new NotImplementedException();
		}

		public override Task RefreshListFromService()
		{
			return Task.CompletedTask;
		}

		public override async Task<bool> UpdateItemInService(UserForView item)
		{
			return await _service.UserPUTAsync(item.Id, item).HandleRequest();
		}

		protected override void EraseParameters()
		{
			// empty
		}
		public override async Task<UserForView> GetItemAsync(int id)
		{
			var email = Preferences.Default.Get("userEmail", "");
			var pass = Preferences.Default.Get("password", "");
			return await Task.FromResult(await LoginUser(email, pass));
		}
	}
}
