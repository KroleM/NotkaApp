using ApiSharedClasses.QueryParameters;
using NotkaDesktop.Helpers;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services.Abstract;
using NotkaDesktop.ViewModels;

namespace NotkaDesktop.Services
{
	public class CurrencyDataStore : AListDataStore<CurrencyForView, CurrencyParameters>
	{
		public CurrencyDataStore()
			:base()
		{
			Params = new CurrencyParameters();
		}

		public override async Task<CurrencyForView> AddItemToService(CurrencyForView item)
		{
			return await _service.CurrencyPOSTAsync(item);
		}

		public override async Task<bool> DeleteItemFromService(CurrencyForView item)
		{
			return await _service.CurrencyDELETEAsync(ApplicationViewModel.s_userId, item.Id).HandleRequest();
		}

		public override async Task<CurrencyForView> Find(CurrencyForView item)
		{
			return await _service.CurrencyGETAsync(ApplicationViewModel.s_userId, item.Id);
		}

		public override async Task<CurrencyForView> Find(int id)
		{
			return await _service.CurrencyGETAsync(ApplicationViewModel.s_userId, id);
		}

		public override async Task RefreshListFromService()
		{
			var PagedList = _service.CurrencyGETAllAsync(ApplicationViewModel.s_userId, Params.PageNumber, Params.PageSize, Params.SortOrder, Params.SearchPhrase).Result;
			Items = PagedList.Items.ToList();
			PageParameters.CurrentPage = PagedList.CurrentPage;
			PageParameters.TotalPages = PagedList.TotalPages;
			PageParameters.PageSize = PagedList.PageSize;
			PageParameters.TotalCount = PagedList.TotalCount;
			PageParameters.HasPrevious = PagedList.HasPrevious;
			PageParameters.HasNext = PagedList.HasNext;
		}

		public override async Task<bool> UpdateItemInService(CurrencyForView item)
		{
			return await _service.CurrencyPUTAsync(item.Id, item).HandleRequest();
		}

		protected override void EraseParameters()
		{
			//throw new NotImplementedException();
		}
	}
}
