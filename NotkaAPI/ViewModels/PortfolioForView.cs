using NotkaAPI.Models;

namespace NotkaAPI.ViewModels
{
	public class PortfolioForView : DictionaryTable
	{
		public int UserId { get; set; }
		public List<StockForView> StocksForView { get; set; } = new();
	}
}
