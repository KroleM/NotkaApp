using System.ComponentModel.DataAnnotations;

namespace NotkaAPI.Models.Investments
{
	public class Currency : DictionaryTable
	{
		[Required]
		public string ShortName { get; set; }
		//public List<Stock> Stocks { get; set; } = new ();	//zakomentować?

	}
}
