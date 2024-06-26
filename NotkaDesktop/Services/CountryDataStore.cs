using ApiSharedClasses.QueryParameters;
using NotkaDesktop.Helpers;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services.Abstract;
using NotkaDesktop.ViewModels;

namespace NotkaDesktop.Services
{
	public class CountryDataStore : AListDataStore<CountryForView, CountryParameters>
	{
		public CountryDataStore()
			: base()
		{
			Params = new CountryParameters();
		}

		public override async Task<CountryForView> AddItemToService(CountryForView item)
		{
			return await _service.CountryPOSTAsync(item);
		}

		public override async Task<bool> DeleteItemFromService(CountryForView item)
		{
			return await _service.CountryDELETEAsync(ApplicationViewModel.s_userId, item.Id).HandleRequest();
		}

		public override async Task<CountryForView> Find(CountryForView item)
		{
			return await _service.CountryGETAsync(ApplicationViewModel.s_userId, item.Id);
		}

		public override async Task<CountryForView> Find(int id)
		{
			return await _service.CountryGETAsync(ApplicationViewModel.s_userId, id);
		}

		public override async Task RefreshListFromService()
		{
			var PagedList = _service.CountryGETAllAsync(ApplicationViewModel.s_userId, Params.PageNumber, Params.PageSize, Params.SortOrder, Params.SearchPhrase).Result;
			Items = PagedList.Items.ToList();
			PageParameters.CurrentPage = PagedList.CurrentPage;
			PageParameters.TotalPages = PagedList.TotalPages;
			PageParameters.PageSize = PagedList.PageSize;
			PageParameters.TotalCount = PagedList.TotalCount;
			PageParameters.HasPrevious = PagedList.HasPrevious;
			PageParameters.HasNext = PagedList.HasNext;
		}

		public override async Task<bool> UpdateItemInService(CountryForView item)
		{
			return await _service.CountryPUTAsync(item.Id, item).HandleRequest();
		}

		protected override void EraseParameters()
		{
			//throw new NotImplementedException();
		}
	}
}
