using System.ComponentModel.DataAnnotations.Schema;

namespace NotkaAPI.Models.Investments
{
	public class StockPrice : DictionaryTable
	{
        public int StockId { get; set; }
        public Stock Stock { get; set; }
		[Column(TypeName = "money")]
		//[DisplayFormat(DataFormatString = "{0:0.##}")]
		public decimal Price { get; set; }
	}
}
