using ApiSharedClasses.QueryParameters;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Contracts
{
	public interface ICountryRepository
	{
		Task<PagedList<CountryForView>> GetCountries(int userId, CountryParameters countryParameters);
		Task<CountryForView> GetCountryById(int userId, int id);
		Task<CountryForView> CreateCountry(CountryForView country);
		Task UpdateCountry(int id, CountryForView country);
		Task DeleteCountry(int userId, int id);
	}
}
