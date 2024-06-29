using ApiSharedClasses.QueryParameters;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Investments;
using NotkaAPI.Models.Users;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Repository
{
	public class StockRepository : RepositoryBase<Stock>, IStockRepository
	{
		public StockRepository(NotkaDatabaseContext repositoryContext) 
			: base(repositoryContext)
		{
		}

		public async Task<PagedList<StockForView>> GetStocks(int userId, StockParameters stockParameters)
		{
			var stocks = Context.Stock.OrderBy(s => s.Ticker).Include(s => s.Currency).Include(s => s.StockExchange);
			//Filtrowanie i wyszukiwanie?

			return await PagedList<StockForView>.CreateAsync(stocks.Select(stock => ModelConverters.ConvertToStockForView(stock)),
										stockParameters.PageNumber,
										stockParameters.PageSize);
		}

		public async Task<StockForView> GetStockById(int userId, int id)
		{
			var stock = await Context.Stock.Include(s => s.Currency).Include(s => s.StockExchange).SingleOrDefaultAsync(s => s.Id == id);

			return ModelConverters.ConvertToStockForView(stock);
		}

		public async Task<StockForView> CreateStock(StockForView stock)
		{
			var stockToAdd = new Stock().CopyProperties(stock);

			Context.Stock.Add(stockToAdd);
			await Context.SaveChangesAsync();

			var result = Context.Stock
				.Include(s => s.Currency)
				.Include(s => s.StockExchange)
				.SingleOrDefault(se => se.Id == stockToAdd.Id);

			return ModelConverters.ConvertToStockForView(result);
		}

		public async Task UpdateStock(int id, StockForView stock)
		{
			var stockToAdd = new Stock().CopyProperties(stock);
			Context.Entry(stockToAdd).State = EntityState.Modified;

			try
			{
				await Context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!StockExists(id))
				{
					throw new NotFoundException();
				}
				else
				{
					throw;
				}
			}
		}

		public async Task DeleteStock(int userId, int id)
		{
			var stock = await Context.Stock.SingleOrDefaultAsync(stock => stock.Id == id);

			if (stock == null)
			{
				throw new NotFoundException();
			}
			//Tylko admin może to zrobić
			if (!Context.User.Any(u => u.Id == userId && u.RoleUsers.Any(ru => ru.RoleId == 3)))
			{
				throw new UnauthorizedException();
			}

			Delete(stock);
			await Context.SaveChangesAsync();
		}

		private bool StockExists(int id)
		{
			return Context.Currency.Any(c => c.Id == id);
		}
	}
}
