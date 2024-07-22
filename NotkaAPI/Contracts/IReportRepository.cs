using ApiSharedClasses.QueryParameters;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Contracts
{
	public interface IReportRepository
	{
		Task<PagedList<ReportStockForView>> GetStocksReport(int userId, ReportParameters reportParameters);
		Task<List<ReportUserForView>> GetUsersReport(int userId, ReportParameters reportParameters); 
	}
}
