using ApiSharedClasses.QueryParameters;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services.Abstract;
using NotkaDesktop.ViewModels;

namespace NotkaDesktop.Services
{
	public class ReportStocksDataStore : AListDataStore<ReportStocksForView, ReportParameters>
	{
		public ReportStocksDataStore()
		{
			Params = new ReportParameters();
			Params.PageSize = 15;
		}

		public override Task<ReportStocksForView> AddItemToService(ReportStocksForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<bool> DeleteItemFromService(ReportStocksForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<ReportStocksForView> Find(ReportStocksForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<ReportStocksForView> Find(int id)
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

		public override Task<bool> UpdateItemInService(ReportStocksForView item)
		{
			throw new NotImplementedException();
		}

		protected override void EraseParameters()
		{
			// empty
		}
	}
}
