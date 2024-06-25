namespace NotkaAPI.Models.Investments
{
	public class StockExchange : DictionaryTable
	{
        public string ShortName { get; set; }
        public List<Stock> Stocks { get; set; } = new();
		public int CountryId { get; set; }
		public Country? Country { get; set; }

	}
}
