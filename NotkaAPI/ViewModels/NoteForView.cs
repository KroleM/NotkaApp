using NotkaAPI.Models;

namespace NotkaAPI.ViewModels
{
	public class NoteForView : DictionaryTable
	{
		public List<TagForView> TagsForView { get; set; } = new();
		//public PictureForView? PictureForView { get; set; }
	}
}
