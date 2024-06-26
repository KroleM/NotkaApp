using NotkaAPI.Models;

namespace NotkaAPI.ViewModels
{
	public class CountryForView : DictionaryTable
	{
		public string ShortName { get; set; } = string.Empty;
		//public List<StockExchangeForView> StockExchangesForView { get; set; } = new();
	}
}
