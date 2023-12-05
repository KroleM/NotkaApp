using NotkaAPI.Models.General;

namespace NotkaAPI.Models.Notes
{
	public class Note : ANote
	{
		public List<NoteTag> NoteTag { get; set; } = new();
        public int? PictureId { get; set; }
        public Picture? Picture { get; set; }
	}
}
