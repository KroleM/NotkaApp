using NotkaAPI.Models.Notes;
using NotkaAPI.Models.Users;

namespace NotkaAPI.Models.General
{
	public class Picture : BaseDatatable
	{
		public byte[] BitPicture { get; set; } = null!;
		public string PictureFormat { get; set; } = null!;
        public bool IsProfile { get; set; }
        public int UserId { get; set; }
        //public User User { get; set; }  // czy potrzebne?
        public int? NoteId { get; set; }
        //public Note? Note { get; set; }  // czy potrzebne?
	}
}
