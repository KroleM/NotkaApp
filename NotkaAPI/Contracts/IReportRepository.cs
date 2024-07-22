using ApiSharedClasses.QueryParameters;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Contracts
{
	public interface IReportRepository
	{
		Task<PagedList<ReportStocksForView>> GetStocksReport(int userId, ReportParameters reportParameters);
	}
}
