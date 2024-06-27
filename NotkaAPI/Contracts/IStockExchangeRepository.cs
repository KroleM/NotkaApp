using ApiSharedClasses.QueryParameters;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Contracts
{
	public interface IStockExchangeRepository
	{
		Task<PagedList<StockExchangeForView>> GetStockExchanges(int userId, StockExchangeParameters stockExchangeParameters);
		Task<StockExchangeForView> GetStockExchangeById(int userId, int id);
		Task<StockExchangeForView> CreateStockExchange(StockExchangeForView stockExchange);
		Task UpdateStockExchange(int id, StockExchangeForView stockExchange);
		Task DeleteStockExchange(int userId, int id);
	}
}
