using ApiSharedClasses.QueryParameters;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Repository
{
	public class ReportRepository : IReportRepository
	{
		protected NotkaDatabaseContext Context { get; }

		public ReportRepository(NotkaDatabaseContext repositoryContext)
		{
			Context = repositoryContext;
		}

		public async Task<PagedList<ReportStocksForView>> GetStocksReport(int userId, ReportParameters reportParameters)
		{
			var reportStocks = Context.PortfolioStock
				.Include(ps => ps.Stock)
				.ThenInclude(s => s.StockExchange)
				.GroupBy(x => x.Stock)
				.Select(stock => new ReportStocksForView
				{
					Name = stock.Key.Name,
					Ticker = stock.Key.Ticker,
					StockExchangeId = stock.Key.StockExchangeId,
					StockExchangeShortName = stock.Key.StockExchange.ShortName ?? string.Empty,
					NumberOfPortfolios = stock.Count()
				})
				.OrderByDescending(x => x.NumberOfPortfolios).ThenBy(x => x.Name);


			return await PagedList<ReportStocksForView>.CreateAsync(reportStocks,
										reportParameters.PageNumber,
										reportParameters.PageSize);
		}

		//GetUsersReport?
	}
}
