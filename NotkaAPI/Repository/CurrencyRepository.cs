using ApiSharedClasses.QueryParameters;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Investments;
using NotkaAPI.Models.Users;
using NotkaAPI.ViewModels;
using System.Data;

namespace NotkaAPI.Repository
{
	public class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
	{
		public CurrencyRepository(NotkaDatabaseContext repositoryContext) 
			: base(repositoryContext)
		{
		}

		public async Task<PagedList<CurrencyForView>> GetCurrencies(int userId, CurrencyParameters currencyParameters)
		{
			var currenciesForView = Context.Currency.Select(currency => ModelConverters.ConvertToCurrencyForView(currency));

			return await PagedList<CurrencyForView>.CreateAsync(currenciesForView.OrderBy(f => f.ShortName),
										currencyParameters.PageNumber,
										currencyParameters.PageSize);
		}

		public async Task<CurrencyForView> GetCurrencyById(int userId, int id)
		{
			var currency = await Context.Currency.SingleOrDefaultAsync(c => c.Id == id);

			return ModelConverters.ConvertToCurrencyForView(currency);
		}

		public async Task<CurrencyForView> CreateCurrency(CurrencyForView currency)
		{
			var currencyToAdd = new Currency().CopyProperties(currency);

			Context.Currency.Add(currencyToAdd);
			await Context.SaveChangesAsync();

			return ModelConverters.ConvertToCurrencyForView(currencyToAdd);
		}

		public async Task UpdateCurrency(int id, CurrencyForView currency)
		{
			var currencyToAdd = new Currency().CopyProperties(currency);
			Context.Entry(currencyToAdd).State = EntityState.Modified;

			try
			{
				await Context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CurrencyExists(id))
				{
					throw new NotFoundException();
				}
				else
				{
					throw;
				}
			}
		}

		public async Task DeleteCurrency(int userId, int id)
		{
			var currency = await Context.Currency.SingleOrDefaultAsync(currency => currency.Id == id);

			if (currency == null)
			{
				throw new NotFoundException();
			}
			//Tylko admin może to zrobić
			if (!Context.User.Any(u => u.Id == userId && u.RoleUsers.Any(ru => ru.RoleId == 3)))
			{
				throw new UnauthorizedException();
			}

			Delete(currency);
			await Context.SaveChangesAsync();
		}

		private bool CurrencyExists(int id)
		{
			return Context.Currency.Any(e => e.Id == id);
		}
	}
}
