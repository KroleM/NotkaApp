using NotkaAPI.Models.Notes;
using NotkaAPI.Models.Users;

namespace NotkaAPI.Models.General
{
	public class Picture : BaseDatatable
	{
		public byte[] BitPicture { get; set; }
		public string PictureFormat { get; set; }
        public bool IsProfile { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
