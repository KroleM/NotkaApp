namespace NotkaAPI.Models.Investments
{
	public class Country : DictionaryTable
	{
		public string ShortName { get; set; } = string.Empty;
		public List<StockExchange> StockExchanges { get; set; } = new();
	}
}
