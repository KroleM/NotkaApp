using ApiSharedClasses.QueryParameters;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Investments;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Repository
{
	public class CountryRepository : RepositoryBase<Country>, ICountryRepository
	{
		public CountryRepository(NotkaDatabaseContext repositoryContext) 
			: base(repositoryContext)
		{
		}

		public async Task<PagedList<CountryForView>> GetCountries(int userId, CountryParameters countryParameters)
		{
			var countries = Context.Country.OrderBy(c => c.Name);
			//FIXME include, gdy pojawi się StockExchangeForView?

			return await PagedList<CountryForView>.CreateAsync(countries.Select(country => ModelConverters.ConvertToCountryForView(country)),
										countryParameters.PageNumber,
										countryParameters.PageSize);
		}

		public async Task<CountryForView> GetCountryById(int userId, int id)
		{
			var country = await Context.Country.SingleOrDefaultAsync(c => c.Id == id);

			return ModelConverters.ConvertToCountryForView(country);
		}

		public async Task<CountryForView> CreateCountry(CountryForView country)
		{
			var countryToAdd = new Country().CopyProperties(country);

			Context.Country.Add(countryToAdd);
			await Context.SaveChangesAsync();

			return ModelConverters.ConvertToCountryForView(countryToAdd);
		}

		public async Task UpdateCountry(int id, CountryForView country)
		{
			var countryToAdd = new Country().CopyProperties(country);
			Context.Entry(countryToAdd).State = EntityState.Modified;

			try
			{
				await Context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CountryExists(id))
				{
					throw new NotFoundException();
				}
				else
				{
					throw;
				}
			}
		}

		public async Task DeleteCountry(int userId, int id)
		{
			var country = await Context.Country.SingleOrDefaultAsync(c => c.Id == id);

			if (country == null)
			{
				throw new NotFoundException();
			}
			//Tylko admin może to zrobić
			if (!Context.User.Any(u => u.Id == userId && u.RoleUsers.Any(ru => ru.RoleId == 3)))
			{
				throw new UnauthorizedException();
			}

			Delete(country);
			await Context.SaveChangesAsync();
		}

		private bool CountryExists(int id)
		{
			return Context.Country.Any(c => c.Id == id);
		}
	}
}
