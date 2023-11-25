namespace NotkaAPI.Models.Investments
{
	public class Currency : DictionaryTable
	{
		public string ShortName { get; set; }
		public List<Stock> Stocks { get; set; } = new ();
	}
}
