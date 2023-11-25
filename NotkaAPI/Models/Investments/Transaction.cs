using NotkaAPI.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotkaAPI.Models.Investments
{
	public class Transaction : BaseDatatable
	{
		public int UserId { get; set; }
		public User User { get; set; }
		public float Quantity { get; set; }
		[Column(TypeName = "money")]
		//[DisplayFormat(DataFormatString = "{0:0.##}")]
		public decimal Price { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
		// To jest tak jakby tabela łącząca User-Stock. W tych klasach nie ma odniesienia do Transaction
    }
}
