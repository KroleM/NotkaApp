using ApiSharedClasses.QueryParameters;
using NotkaDesktop.Helpers;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services.Abstract;
using NotkaDesktop.ViewModels;

namespace NotkaMobile.Services
{
	public class UserDataStore : AListDataStore<UserForView, UserParameters>
	{
		public UserDataStore()
			: base()
		{ 
			Params = new UserParameters();
		}
		public async Task<UserForView> LoginUser(string email, string passwordHash)
		{
			return await _service.UserGETWithAuthAsync(email, passwordHash);
		}

		public override async Task<UserForView> AddItemToService(UserForView item)
		{
			return await _service.UserPOSTAsync(item);
		}

		public override async Task<bool> DeleteItemFromService(UserForView item)
		{
			return await _service.UserDELETEAsync(ApplicationViewModel.s_userId, item.Id).HandleRequest();
			//return true;
		}

		public override async Task<UserForView> Find(UserForView item)
		{
			return await _service.UserGETAsync(ApplicationViewModel.s_userId, item.Id);
		}

		public override async Task<UserForView> Find(int id)
		{
			return await _service.UserGETAsync(ApplicationViewModel.s_userId, id);
		}

		public override async Task RefreshListFromService()
		{
			//return Task.CompletedTask;
			var PagedList = _service.UserGETAllAsync(ApplicationViewModel.s_userId, Params.PageNumber, Params.PageSize, Params.SortOrder, Params.SearchPhrase).Result;
			Items = PagedList.Items.ToList();
			PageParameters.CurrentPage = PagedList.CurrentPage;
			PageParameters.TotalPages = PagedList.TotalPages;
			PageParameters.PageSize = PagedList.PageSize;
			PageParameters.TotalCount = PagedList.TotalCount;
			PageParameters.HasPrevious = PagedList.HasPrevious;
			PageParameters.HasNext = PagedList.HasNext;
		}

		public override async Task<bool> UpdateItemInService(UserForView item)
		{
			return await _service.UserPUTAsync(item.Id, item).HandleRequest();
		}

		protected override void EraseParameters()
		{
			// empty
		}

	}
}
