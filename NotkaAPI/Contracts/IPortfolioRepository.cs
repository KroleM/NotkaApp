using ApiSharedClasses.QueryParameters;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Contracts
{
	public interface IPortfolioRepository
	{
		Task<PagedList<PortfolioForView>> GetPortfolios(int userId, PortfolioParameters portfolioParameters);
		Task<PortfolioForView> GetPortfolioById(int userId, int id);
		Task<PortfolioForView> GetPortfolioByUser(int userId);
		Task<PortfolioForView> CreatePortfolio(PortfolioForView portfolio);
		Task UpdatePortfolio(int id, PortfolioForView portfolio);
		Task DeletePortfolio(int userId, int id);
	}
}
