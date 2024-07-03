using ApiSharedClasses.QueryParameters;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;

namespace NotkaMobile.Services
{
	public class StockExchangeDataStore : AListDataStore<StockExchangeForView, StockExchangeParameters>
	{
		public StockExchangeDataStore()
			: base()
		{
			Params = new StockExchangeParameters();
		}

		public override Task<StockExchangeForView> AddItemToService(StockExchangeForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<bool> DeleteItemFromService(StockExchangeForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<StockExchangeForView> Find(StockExchangeForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<StockExchangeForView> Find(int id)
		{
			throw new NotImplementedException();
		}

		public async override Task RefreshListFromService()
		{
			var PagedList = _service.StockExchangeGETAllAsync(Preferences.Default.Get("userId", 0), Params.PageNumber, Params.PageSize, Params.SortOrder, Params.SearchPhrase).Result;
			Items = PagedList.Items.ToList();
			PageParameters.CurrentPage = PagedList.CurrentPage;
			PageParameters.TotalPages = PagedList.TotalPages;
			PageParameters.PageSize = PagedList.PageSize;
			PageParameters.TotalCount = PagedList.TotalCount;
			PageParameters.HasPrevious = PagedList.HasPrevious;
			PageParameters.HasNext = PagedList.HasNext;
		}

		public override Task<bool> UpdateItemInService(StockExchangeForView item)
		{
			throw new NotImplementedException();
		}

		protected override void EraseParameters()
		{
			//throw new NotImplementedException();
		}
	}
}
