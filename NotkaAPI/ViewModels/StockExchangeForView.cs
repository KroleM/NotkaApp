using NotkaAPI.Models;

namespace NotkaAPI.ViewModels
{
	public class StockExchangeForView : DictionaryTable
	{
		public string ShortName { get; set; }
		public List<StockForView> StocksForView { get; set; } = new();
		public int CountryId { get; set; }
		public string CountryShortName { get; set; } = string.Empty;
		//public Country? Country { get; set; }
	}
}
