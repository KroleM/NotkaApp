using ApiSharedClasses.QueryParameters;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Migrations;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Investments;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Repository
{
	public class StockExchangeRepository : RepositoryBase<StockExchange>, IStockExchangeRepository
	{
		public StockExchangeRepository(NotkaDatabaseContext repositoryContext) 
			: base(repositoryContext)
		{
		}

		public async Task<PagedList<StockExchangeForView>> GetStockExchanges(int userId, StockExchangeParameters stockExchangeParameters)
		{
			var stockExchanges = Context.StockExchange.OrderBy(se => se.ShortName).Include(se => se.Country);
			//FIXME include, gdy pojawi się StockExchangeForView?

			return await PagedList<StockExchangeForView>.CreateAsync(stockExchanges.Select(stockEx => ModelConverters.ConvertToStockExchangeForView(stockEx)),
										stockExchangeParameters.PageNumber,
										stockExchangeParameters.PageSize);
		}

		public async Task<StockExchangeForView> GetStockExchangeById(int userId, int id)
		{
			var stockExchange = await Context.StockExchange.Include(se => se.Country).SingleOrDefaultAsync(se => se.Id == id);	//do sprawdzenia

			return ModelConverters.ConvertToStockExchangeForView(stockExchange);
		}

		public async Task<StockExchangeForView> CreateStockExchange(StockExchangeForView stockExchange)
		{
			var stockExchangeToAdd = new StockExchange().CopyProperties(stockExchange);

			Context.StockExchange.Add(stockExchangeToAdd);
			await Context.SaveChangesAsync();

			var result = Context.StockExchange.Include(se => se.Country).SingleOrDefault(se => se.Id == stockExchangeToAdd.Id);

			//return ModelConverters.ConvertToStockExchangeForView(stockExchangeToAdd);
			return ModelConverters.ConvertToStockExchangeForView(result);
		}

		public async Task UpdateStockExchange(int id, StockExchangeForView stockExchange)
		{
			var stockExchangeToAdd = new StockExchange().CopyProperties(stockExchange);
			Context.Entry(stockExchangeToAdd).State = EntityState.Modified;

			try
			{
				await Context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!StockExchangeExists(id))
				{
					throw new NotFoundException();
				}
				else
				{
					throw;
				}
			}
		}

		public async Task DeleteStockExchange(int userId, int id)
		{
			var stockExchange = await Context.StockExchange.SingleOrDefaultAsync(se => se.Id == id);

			if (stockExchange == null)
			{
				throw new NotFoundException();
			}
			//Tylko admin może to zrobić
			if (!Context.User.Any(u => u.Id == userId && u.RoleUsers.Any(ru => ru.RoleId == 3)))
			{
				throw new UnauthorizedException();
			}

			Delete(stockExchange);
			await Context.SaveChangesAsync();
		}

		private bool StockExchangeExists(int id)
		{
			return Context.StockExchange.Any(se => se.Id == id);
		}
	}
}
