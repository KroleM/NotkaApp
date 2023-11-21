
namespace NotkaAPI.Models.Notes
{
	public class NoteTag : BaseDatatable
	{
		public int NoteId { get; set; }
		public Note? Note { get; set; }
		public int TagId { get; set; }
		public Tag? Tag { get; set; }
	}
}
