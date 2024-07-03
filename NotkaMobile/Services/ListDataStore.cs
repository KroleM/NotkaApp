using ApiSharedClasses.QueryParameters;
using NotkaMobile.Helpers;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;

namespace NotkaMobile.Services
{
	public class ListDataStore : AListDataStore<ListForView, ListParameters>
	{
		public ListDataStore()
			: base()
		{
			Params = new ListParameters();
		}

		public override async Task<ListForView> AddItemToService(ListForView item)
		{
			return await _service.ListPOSTAsync(item);
		}

		public async override Task<bool> DeleteItemFromService(ListForView item)
		{
			return await _service.ListDELETEAsync(Preferences.Default.Get("userId", 0), item.Id).HandleRequest();
		}

		public async override Task<ListForView> Find(ListForView item)
		{
			return await _service.ListGETAsync(Preferences.Default.Get("userId", 0), item.Id);
		}

		public async override Task<ListForView> Find(int id)
		{
			return await _service.ListGETAsync(Preferences.Default.Get("userId", 0), id);
		}

		public async override Task RefreshListFromService()
		{
			var PagedList = _service.ListGETAllAsync(Preferences.Default.Get("userId", 0),
								Params.PageNumber, Params.PageSize, Params.SortOrder, Params.SearchPhrase)
							.Result;
			Items = PagedList.Items.ToList();
			PageParameters.CurrentPage = PagedList.CurrentPage;
			PageParameters.TotalPages = PagedList.TotalPages;
			PageParameters.PageSize = PagedList.PageSize;
			PageParameters.TotalCount = PagedList.TotalCount;
			PageParameters.HasPrevious = PagedList.HasPrevious;
			PageParameters.HasNext = PagedList.HasNext;
		}

		public async override Task<bool> UpdateItemInService(ListForView item)
		{
			return await _service.ListPUTAsync(item.Id, item).HandleRequest();
		}

		protected override void EraseParameters()
		{
			Params = new ListParameters();
		}
	}
}
