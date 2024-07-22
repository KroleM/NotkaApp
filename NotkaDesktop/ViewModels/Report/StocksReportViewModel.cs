using ApiSharedClasses.QueryParameters;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;

namespace NotkaDesktop.ViewModels
{
	public class StocksReportViewModel : AListViewModel<ReportStocksForView, ReportParameters>
	{
		public StocksReportViewModel(ReportStocksDataStore dataStore) 
			: base("Najpopularniejsze spółki", dataStore)
		{
		}

		public override Task GoToAddPage()
		{
			throw new NotImplementedException();
		}

		public override Task OnDeleteItem()
		{
			throw new NotImplementedException();
		}

		public override Task OnEditItem()
		{
			throw new NotImplementedException();
		}
	}
}
