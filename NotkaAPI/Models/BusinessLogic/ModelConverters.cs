using NotkaAPI.Helpers;
using NotkaAPI.Models.General;
using NotkaAPI.Models.Notes;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Models.BusinessLogic
{
	public static class ModelConverters
	{
		public static NoteForView ConvertToNoteForView(Note note)
		{
			return new NoteForView
			{
				TagsForView = note?.NoteTag.Select(notetag => new TagForView().CopyProperties(notetag.Tag)).ToList() ?? new(),
				//PictureForView = note?.Picture ?? new PictureForView().CopyProperties(note?.Picture),
			}.CopyProperties(note);
		}
	}
}
