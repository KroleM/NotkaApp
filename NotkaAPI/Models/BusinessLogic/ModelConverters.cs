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
				TagsForView = note?.NoteTags.Select(notetag => new TagForView().CopyProperties(notetag.Tag)).ToList() ?? new(),
			}.CopyProperties(note);
		}
		public static TagForView ConvertToTagForView(Tag tag)
		{
			return new TagForView
			{
				//NotesForView = tag?.NoteTags.Select(notetag => new NoteForView().CopyProperties(notetag.Note)).ToList() ?? new(),
				NotesForView = tag?.NoteTags.Select(notetag => ConvertToNoteForView(notetag?.Note)).ToList() ?? new(),
			}.CopyProperties(tag);
		}
	}
}
