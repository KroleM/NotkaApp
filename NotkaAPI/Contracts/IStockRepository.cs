using ApiSharedClasses.QueryParameters;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Contracts
{
	public interface IStockRepository
	{
		Task<PagedList<StockForView>> GetStocks(int userId, StockParameters stockParameters);
		Task<StockForView> GetStockById(int userId, int id);
		Task<StockForView> CreateStock(StockForView stock);
		Task UpdateStock(int id, StockForView stock);
		Task DeleteStock(int userId, int id);
	}
}
