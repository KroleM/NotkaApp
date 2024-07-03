using ApiSharedClasses.QueryParameters;
using ApiSharedClasses.SortValues;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Investments;
using NotkaAPI.Models.Notes;
using NotkaAPI.Models.Users;
using NotkaAPI.ViewModels;
using System.Diagnostics;
using static Azure.Core.HttpHeader;

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
			//var stocks = Context.Stock.AsQueryable();	//.OrderBy(s => s.Ticker).Include(s => s.Currency).Include(s => s.StockExchange);
			//Filtrowanie i wyszukiwanie?
			var stocks = FindByCondition(s => stockParameters.StockExchangeId == 0 ? s.StockExchangeId > 0 : s.StockExchangeId == stockParameters.StockExchangeId)
							.Where(s => stockParameters.CurrencyId == 0 ? s.CurrencyId > 0 : s.CurrencyId == stockParameters.CurrencyId)
							.Where(s => stockParameters.IsActive == null ? true : s.IsActive == stockParameters.IsActive)
							.AsQueryable();

			SearchByPhrase(ref stocks, stockParameters.SearchPhrase);

			ApplySort(ref stocks, stockParameters.SortOrder);

			var stocksWithIncludes = stocks.Include(s => s.Currency).Include(s => s.StockExchange);

			return await PagedList<StockForView>.CreateAsync(stocksWithIncludes.Select(stock => ModelConverters.ConvertToStockForView(stock)),
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

		private void SearchByPhrase(ref IQueryable<Stock> stocks, string? searchPhrase)
		{
			if (!stocks.Any() || string.IsNullOrWhiteSpace(searchPhrase))
				return;
			stocks = stocks.Where(s => s.Name.ToLower().Contains(searchPhrase.Trim().ToLower())
						|| s.Ticker.ToLower().Contains(searchPhrase.Trim().ToLower()));
		}

		private void ApplySort(ref IQueryable<Stock> stocks, string? orderByString)
		{
			if (!stocks.Any())
				return;

			StockSortValue stockSortEnum = new();

			if (Enum.TryParse(orderByString, out stockSortEnum))
			{
				switch (stockSortEnum)
				{
					case StockSortValue.TickerFromAtoZ:
						stocks = stocks.OrderBy(x => x.Ticker).ThenByDescending(x => x.ModifiedDate);
						break;
					case StockSortValue.TickerFromZtoA:
						stocks = stocks.OrderByDescending(x => x.Ticker).ThenByDescending(x => x.ModifiedDate);
						break;
					case StockSortValue.NameFromAtoZ:
						stocks = stocks.OrderBy(x => x.Name).ThenByDescending(x => x.ModifiedDate);
						break;
					case StockSortValue.NameFromZtoA:
						stocks = stocks.OrderByDescending(x => x.Name).ThenByDescending(x => x.ModifiedDate);
						break;
				}
			}
			else
			{
				stocks = stocks.OrderBy(x => x.Name);
			}
		}
	}
}
