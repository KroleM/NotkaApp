using ApiSharedClasses.QueryParameters;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services.Abstract;
using NotkaDesktop.ViewModels;

namespace NotkaDesktop.Services
{
	public class ReportStocksDataStore : AListDataStore<ReportStockForView, ReportParameters>
	{
		public ReportStocksDataStore()
		{
			Params = new ReportParameters();
			Params.PageSize = 15;
		}

		public override Task<ReportStockForView> AddItemToService(ReportStockForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<bool> DeleteItemFromService(ReportStockForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<ReportStockForView> Find(ReportStockForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<ReportStockForView> Find(int id)
		{
			throw new NotImplementedException();
		}

		public override async Task RefreshListFromService()
		{
			var PagedList = _service.GETStocksReportAsync(ApplicationViewModel.s_userId,
					Params.PageNumber, Params.PageSize, Params.SortOrder, Params.SearchPhrase).Result;
			Items = PagedList.Items.ToList();
			PageParameters.CurrentPage = PagedList.CurrentPage;
			PageParameters.TotalPages = PagedList.TotalPages;
			PageParameters.PageSize = PagedList.PageSize;
			PageParameters.TotalCount = PagedList.TotalCount;
			PageParameters.HasPrevious = PagedList.HasPrevious;
			PageParameters.HasNext = PagedList.HasNext;
		}

		public override Task<bool> UpdateItemInService(ReportStockForView item)
		{
			throw new NotImplementedException();
		}

		protected override void EraseParameters()
		{
			// empty
		}
	}
}
