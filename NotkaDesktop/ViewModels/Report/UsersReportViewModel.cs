using ApiSharedClasses.QueryParameters;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;

namespace NotkaDesktop.ViewModels
{
	public class UsersReportViewModel : AListViewModel<ReportUserForView, ReportParameters>
	{
		public UsersReportViewModel(ReportUsersDataStore dataStore) 
			: base("Statystyki użytkowników", dataStore)
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
