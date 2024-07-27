using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NotkaAPI.Models.Investments
{
	[Index(nameof(Ticker), IsUnique = true)]
	public class Stock : DictionaryTable
	{
		[Required]
		[StringLength(6)]
		public string Ticker { get; set; }
        public int StockExchangeId { get; set; }
        public StockExchange StockExchange { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public List<StockPrice> StockPrices { get; set; } = new();
		public List<StockNote> StockNotes { get; set; } = new();
        public List<PortfolioStock> PortfolioStocks { get; set; } = new();
        // Powiązane spółki??
    }
}
