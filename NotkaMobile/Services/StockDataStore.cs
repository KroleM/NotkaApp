using ApiSharedClasses.QueryParameters;
using NotkaMobile.Helpers;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;

namespace NotkaMobile.Services
{
	public class StockDataStore : AListDataStore<StockForView, StockParameters>
	{
		public StockDataStore()
			: base()
		{
			Params = new StockParameters();
		}

		public override async Task<StockForView> AddItemToService(StockForView item)
		{
			return await _service.StockPOSTAsync(item);
		}

		public override async Task<bool> DeleteItemFromService(StockForView item)
		{
			return await _service.StockDELETEAsync(Preferences.Default.Get("userId", 0), item.Id).HandleRequest();
		}

		public override async Task<StockForView> Find(StockForView item)
		{
			return await _service.StockGETAsync(Preferences.Default.Get("userId", 0), item.Id);
		}

		public override async Task<StockForView> Find(int id)
		{
			return await _service.StockGETAsync(Preferences.Default.Get("userId", 0), id);
		}

		public override async Task RefreshListFromService()
		{
			var PagedList = _service.StockGETAllAsync(Preferences.Default.Get("userId", 0), Params.PageNumber, Params.PageSize, Params.SortOrder, Params.SearchPhrase).Result;
			Items = PagedList.Items.ToList();
			PageParameters.CurrentPage = PagedList.CurrentPage;
			PageParameters.TotalPages = PagedList.TotalPages;
			PageParameters.PageSize = PagedList.PageSize;
			PageParameters.TotalCount = PagedList.TotalCount;
			PageParameters.HasPrevious = PagedList.HasPrevious;
			PageParameters.HasNext = PagedList.HasNext;
		}

		public override async Task<bool> UpdateItemInService(StockForView item)
		{
			return await _service.StockPUTAsync(item.Id, item).HandleRequest();
		}

		protected override void EraseParameters()
		{
			//throw new NotImplementedException();
		}
	}
}
