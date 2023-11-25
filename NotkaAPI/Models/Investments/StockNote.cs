using NotkaAPI.Models.Notes;

namespace NotkaAPI.Models.Investments
{
	public class StockNote : BaseDatatable
	{
        public int StockId { get; set; }
		public Stock Stock { get; set;}
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
