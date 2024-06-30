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
		public List<NoteForView> NotesForViews { get; set; } = new();

	}
}
