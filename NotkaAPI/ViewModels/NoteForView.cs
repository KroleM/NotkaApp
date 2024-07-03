using NotkaAPI.Models;
using NotkaAPI.Models.General;

namespace NotkaAPI.ViewModels
{
	public class NoteForView : DictionaryTable
	{
		public int UserId { get; set; }
		public List<TagForView> TagsForView { get; set; } = new();  //FIXME new() może nie być konieczne
		public Picture? Picture { get; set; }
		public int? StockId { get; set; }
	}
}
