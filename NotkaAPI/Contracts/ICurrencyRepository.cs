using ApiSharedClasses.QueryParameters;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Contracts
{
	public interface ICurrencyRepository
	{
		Task<PagedList<CurrencyForView>> GetCurrencies(int userId, CurrencyParameters currencyParameters);
		Task<CurrencyForView> GetCurrencyById(int userId, int id);
		Task<CurrencyForView> CreateCurrency(CurrencyForView currency);
		Task UpdateCurrency(int id, CurrencyForView currency);
		Task DeleteCurrency(int userId, int id);
	}
}
