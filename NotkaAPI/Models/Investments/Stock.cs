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
        public List<StockNote> StockNotes { get; set; } = new();
	}
}
