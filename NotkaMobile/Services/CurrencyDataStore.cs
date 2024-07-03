using ApiSharedClasses.QueryParameters;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;

namespace NotkaMobile.Services
{
	public class CurrencyDataStore : AListDataStore<CurrencyForView, CurrencyParameters>
	{
		public CurrencyDataStore()
			: base()
		{
			Params = new CurrencyParameters();
		}

		public override Task<CurrencyForView> AddItemToService(CurrencyForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<bool> DeleteItemFromService(CurrencyForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<CurrencyForView> Find(CurrencyForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<CurrencyForView> Find(int id)
		{
			throw new NotImplementedException();
		}

		public async override Task RefreshListFromService()
		{
			var PagedList = _service.CurrencyGETAllAsync(Preferences.Default.Get("userId", 0), Params.PageNumber, Params.PageSize, Params.SortOrder, Params.SearchPhrase).Result;
			Items = PagedList.Items.ToList();
			PageParameters.CurrentPage = PagedList.CurrentPage;
			PageParameters.TotalPages = PagedList.TotalPages;
			PageParameters.PageSize = PagedList.PageSize;
			PageParameters.TotalCount = PagedList.TotalCount;
			PageParameters.HasPrevious = PagedList.HasPrevious;
			PageParameters.HasNext = PagedList.HasNext;
		}

		public override Task<bool> UpdateItemInService(CurrencyForView item)
		{
			throw new NotImplementedException();
		}

		protected override void EraseParameters()
		{
			//throw new NotImplementedException();
		}
	}
}
