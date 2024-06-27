using ApiSharedClasses.QueryParameters;
using NotkaDesktop.Helpers;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services.Abstract;
using NotkaDesktop.ViewModels;

namespace NotkaDesktop.Services
{
	public class StockExchangeDataStore : AListDataStore<StockExchangeForView, StockExchangeParameters>
	{
		public StockExchangeDataStore()
			: base()
		{
			Params = new StockExchangeParameters();
		}
		
		public override async Task<StockExchangeForView> AddItemToService(StockExchangeForView item)
		{
			return await _service.StockExchangePOSTAsync(item);
		}

		public override async Task<bool> DeleteItemFromService(StockExchangeForView item)
		{
			return await _service.StockExchangeDELETEAsync(ApplicationViewModel.s_userId, item.Id).HandleRequest();
		}

		public override async Task<StockExchangeForView> Find(StockExchangeForView item)
		{
			return await _service.StockExchangeGETAsync(ApplicationViewModel.s_userId, item.Id);
		}

		public override async Task<StockExchangeForView> Find(int id)
		{
			return await _service.StockExchangeGETAsync(ApplicationViewModel.s_userId, id);
		}

		public override async Task RefreshListFromService()
		{
			var PagedList = _service.StockExchangeGETAllAsync(ApplicationViewModel.s_userId, Params.PageNumber, Params.PageSize, Params.SortOrder, Params.SearchPhrase).Result;
			Items = PagedList.Items.ToList();
			PageParameters.CurrentPage = PagedList.CurrentPage;
			PageParameters.TotalPages = PagedList.TotalPages;
			PageParameters.PageSize = PagedList.PageSize;
			PageParameters.TotalCount = PagedList.TotalCount;
			PageParameters.HasPrevious = PagedList.HasPrevious;
			PageParameters.HasNext = PagedList.HasNext;
		}

		public override async Task<bool> UpdateItemInService(StockExchangeForView item)
		{
			return await _service.StockExchangePUTAsync(item.Id, item).HandleRequest();
		}

		protected override void EraseParameters()
		{
			//throw new NotImplementedException();
		}
	}
}
