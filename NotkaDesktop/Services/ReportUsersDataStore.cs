using ApiSharedClasses.QueryParameters;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services.Abstract;
using NotkaDesktop.ViewModels;

namespace NotkaDesktop.Services
{
	public class ReportUsersDataStore : AListDataStore<ReportUserForView, ReportParameters>
	{
		public ReportUsersDataStore()
		{
			Params = new ReportParameters();
		}

		public override Task<ReportUserForView> AddItemToService(ReportUserForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<bool> DeleteItemFromService(ReportUserForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<ReportUserForView> Find(ReportUserForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<ReportUserForView> Find(int id)
		{
			throw new NotImplementedException();
		}

		public override async Task RefreshListFromService()
		{
			var reportUserList = _service.GETUsersReportAsync(ApplicationViewModel.s_userId,
				Params.PageNumber, Params.PageSize, Params.SortOrder, Params.SearchPhrase).Result;
			Items = reportUserList.ToList();
			//PageParameters.CurrentPage = PagedList.CurrentPage;
			//PageParameters.TotalPages = PagedList.TotalPages;
			//PageParameters.PageSize = PagedList.PageSize;
			//PageParameters.TotalCount = PagedList.TotalCount;
			//PageParameters.HasPrevious = PagedList.HasPrevious;
			//PageParameters.HasNext = PagedList.HasNext;
		}

		public override Task<bool> UpdateItemInService(ReportUserForView item)
		{
			throw new NotImplementedException();
		}

		protected override void EraseParameters()
		{
			//throw new NotImplementedException();
		}
	}
}
