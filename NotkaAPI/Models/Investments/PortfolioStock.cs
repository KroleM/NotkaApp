using System.ComponentModel.DataAnnotations.Schema;

namespace NotkaAPI.Models.Investments
{
	public class PortfolioStock : BaseDatatable
	{
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public float Quantity { get; set; }
		[Column(TypeName = "money")]
		//[DisplayFormat(DataFormatString = "{0:0.##}")]
		public decimal? AveragePrice { get; set; }
	}
}
