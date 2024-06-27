using NotkaAPI.Models;
using NotkaAPI.Models.Investments;

namespace NotkaAPI.ViewModels
{
	public class StockForView : DictionaryTable
	{
		public string Ticker { get; set; }
		public int StockExchangeId { get; set; }
		public string StockExchangeShortName { get; set; }
		public int CurrencyId { get; set; }
		public string CurrencyShortName { get; set; }
		//public List<StockPrice> StockPrices { get; set; } = new();
		//public List<StockNote> StockNotes { get; set; } = new();
		//public List<PortfolioStock> PortfolioStocks { get; set; } = new();
		//public List<WatchlistStock> WatchlistStocks { get; set; } = new();
	}
}
