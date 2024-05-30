using NotkaAPI.Models;

namespace NotkaAPI.ViewModels
{
	public class TagForView : DictionaryTable
	{
		public int UserId { get; set; }
		public List<NoteForView> NotesForView { get; set; } = new();
		public List<ListForView> ListsForView { get; set; } = new();
	}
}
