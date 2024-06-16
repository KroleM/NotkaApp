using ApiSharedClasses.QueryParameters;
using NotkaDesktop.Helpers;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services.Abstract;
using NotkaDesktop.ViewModels;

namespace NotkaDesktop.Services
{
	public class RoleDataStore : AListDataStore<RoleForView, RoleParameters>
	{
		public RoleDataStore()
			: base()
		{
			Params = new RoleParameters();
		}

		public override async Task<RoleForView> AddItemToService(RoleForView item)
		{
			return await _service.RolePOSTAsync(item);
		}

		public override async Task<bool> DeleteItemFromService(RoleForView item)
		{
			return await _service.RoleDELETEAsync(ApplicationViewModel.s_userId, item.Id).HandleRequest();
		}

		public override async Task<RoleForView> Find(RoleForView item)
		{
			return await _service.RoleGETAsync(ApplicationViewModel.s_userId, item.Id);
		}

		public override async Task<RoleForView> Find(int id)
		{
			return await _service.RoleGETAsync(ApplicationViewModel.s_userId, id);
		}

		public override async Task RefreshListFromService()
		{
			var PagedList = _service.RoleGETAllAsync(ApplicationViewModel.s_userId, Params.PageNumber, Params.PageSize, Params.SortOrder, Params.SearchPhrase).Result;
			Items = PagedList.Items.ToList();
			PageParameters.CurrentPage = PagedList.CurrentPage;
			PageParameters.TotalPages = PagedList.TotalPages;
			PageParameters.PageSize = PagedList.PageSize;
			PageParameters.TotalCount = PagedList.TotalCount;
			PageParameters.HasPrevious = PagedList.HasPrevious;
			PageParameters.HasNext = PagedList.HasNext;
		}

		public override async Task<bool> UpdateItemInService(RoleForView item)
		{
			return await _service.RolePUTAsync(item.Id, item).HandleRequest();
		}

		protected override void EraseParameters()
		{
			//
		}
	}
}
