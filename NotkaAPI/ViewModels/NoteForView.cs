using NotkaAPI.Models;

namespace NotkaAPI.ViewModels
{
	public class NoteForView : DictionaryTable
	{
		public List<TagForView> TagsForView { get; set; } = new();	//FIXME new() może nie być konieczne
		//public PictureForView? PictureForView { get; set; }
		//public Picture? Picture { get; set; }
	}
}
