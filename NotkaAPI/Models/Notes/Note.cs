using NotkaAPI.Models.General;

namespace NotkaAPI.Models.Notes
{
	public class Note : ANote
	{
		public List<NoteTag> NoteTag { get; set; } = new();
		public List<Picture> Pictures { get; set; } = new();
	}
}
