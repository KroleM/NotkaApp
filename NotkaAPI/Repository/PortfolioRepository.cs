using ApiSharedClasses.QueryParameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Investments;
using NotkaAPI.Models.Notes;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Repository
{
	public class PortfolioRepository : RepositoryBase<Portfolio>, IPortfolioRepository
	{
		public PortfolioRepository(NotkaDatabaseContext repositoryContext) 
			: base(repositoryContext)
		{
		}

		public async Task<PagedList<PortfolioForView>> GetPortfolios(int userId, PortfolioParameters portfolioParameters)
		{
			throw new NotImplementedException();
		}

		public async Task<PortfolioForView> GetPortfolioById(int userId, int id)
		{
			if (!await Context.Portfolio.AnyAsync(p => p.Id == id))
			{
				throw new NotFoundException();
			}
			var portfolio = await Context.Portfolio
					.Include(portfolio => portfolio.PortfolioStocks)
					.ThenInclude(ps => ps.Stock)
					.ThenInclude(s => s.Currency)
					.Include(portfolio => portfolio.PortfolioStocks)
					.ThenInclude(ps => ps.Stock)
					.ThenInclude(s => s.StockExchange)
					.SingleOrDefaultAsync(p => p.Id == id);

			if (portfolio.UserId != userId)
			{
				throw new UnauthorizedException();
			}

			return ModelConverters.ConvertToPortfolioForView(portfolio);
		}

		public async Task<PortfolioForView> GetPortfolioByUser(int userId)
		{
			if (!await Context.Portfolio.AnyAsync(p => p.UserId == userId))
			{
				throw new NotFoundException();
			}
			var portfolio = await Context.Portfolio
					.Include(portfolio => portfolio.PortfolioStocks)
					.ThenInclude(ps => ps.Stock)
					.ThenInclude(s => s.Currency)
					.Include(portfolio => portfolio.PortfolioStocks)
					.ThenInclude(ps => ps.Stock)
					.ThenInclude(s => s.StockExchange)
					.FirstOrDefaultAsync(p => p.UserId == userId);

			return ModelConverters.ConvertToPortfolioForView(portfolio);
		}

		public async Task<PortfolioForView> CreatePortfolio(PortfolioForView portfolio)
		{
			var portfolioToAdd = new Portfolio().CopyProperties(portfolio);

			using (var dbContextTransaction = Context.Database.BeginTransaction())
			{
				//This goes first, so that later portfolioToUpdate.Id can be extracted
				//Context.Note.Add(portfolioToUpdate);
				Create(portfolioToAdd);
				await Context.SaveChangesAsync();

				if (!portfolio.StocksForView.IsNullOrEmpty())
				{
					foreach (var stockForView in portfolio.StocksForView)
					{
						await AddToContextPortfolioStockAsync(stockForView, portfolioToAdd.Id);
						await Context.SaveChangesAsync();
					}
				}
				dbContextTransaction.Commit();
			}

			var uploadedPortfolio = await Context.Portfolio
					.Include(portfolio => portfolio.PortfolioStocks)
					.ThenInclude(ps => ps.Stock)
					.FirstOrDefaultAsync(p => p.Id == portfolioToAdd.Id);

			return ModelConverters.ConvertToPortfolioForView(uploadedPortfolio);
		}

		public async Task UpdatePortfolio(int id, PortfolioForView portfolio)
		{
			var portfolioToUpdate = new Portfolio().CopyProperties(portfolio);
			Context.Entry(portfolioToUpdate).State = EntityState.Modified;

			try
			{
				// V1 - kasowanie rekordów z tabeli łączącej PortfolioStock i ponowne dodawanie
				//using (var dbContextTransaction = Context.Database.BeginTransaction())
				//{
				//	//PortfolioStocks
				//	Context.PortfolioStock.RemoveRange(await Context.PortfolioStock.Where(ps => ps.PortfolioId == portfolioToUpdate.Id).ToArrayAsync());
				//	if (!portfolio.StocksForView.IsNullOrEmpty())
				//	{
				//		foreach (var stockForView in portfolio.StocksForView)
				//		{
				//			await AddToContextPortfolioStockAsync(stockForView, portfolioToUpdate.Id);
				//		}
				//	}

				//	await Context.SaveChangesAsync();
				//	dbContextTransaction.Commit();
				//}

				// V2 - Dodaje rekordy do tabeli PortfolioStocks, ale tylko jeśli dana spółka nie była jeszcze przyporządkowana do tego Portfolio
				using (var dbContextTransaction = Context.Database.BeginTransaction())  //czy to konieczne?
				{
					//PortfolioStocks
					var stockIds = await Context.PortfolioStock.Where(ps => ps.PortfolioId == portfolio.Id).Select(ps => ps.StockId).ToListAsync();

					if (!portfolio.StocksForView.IsNullOrEmpty())
					{
						foreach (var stockForView in portfolio.StocksForView)
						{
							if (stockIds.Contains(stockForView.Id))
							{
								stockIds.Remove(stockForView.Id);
								continue;
							}
							await AddToContextPortfolioStockAsync(stockForView, portfolioToUpdate.Id);
						}
					}
					foreach (var stockId in stockIds)
					{
						Context.PortfolioStock.Remove(
							await Context.PortfolioStock.Where(ps => ps.StockId == stockId && ps.PortfolioId == portfolioToUpdate.Id).FirstAsync());
					}
					//FIXME brakuje usunięcia usuniętych akcji
					await Context.SaveChangesAsync();
					dbContextTransaction.Commit();
				}
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PortfolioExists(id))
				{
					throw new NotFoundException();
				}
				else
				{
					throw;
				}
			}
		}

		public async Task DeletePortfolio(int userId, int id)
		{
			throw new NotImplementedException();
		}

		private async Task AddToContextPortfolioStockAsync(StockForView stockForView, int portfolioId)
		{
			//var stock = new Stock().CopyProperties(stockForView); //do usunięcia?

			Context.PortfolioStock.Add(new PortfolioStock
			{
				Id = 0,
				IsActive = true,
				PortfolioId = portfolioId,
				StockId = stockForView.Id,
			});
		}

		private bool PortfolioExists(int id)
		{
			return Context.Portfolio.Any(p => p.Id == id);
		}
	}
}
