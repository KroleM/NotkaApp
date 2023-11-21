using Notka.Database.Data.Notes;
using Notka.Database.Data.Users;

namespace Notka.Database.Data.General
{
	public class Picture : BaseDatatable
	{
		public byte[] BitPicture { get; set; }
		public string PictureFormat { get; set; }
        public bool IsProfile { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
		public List<Note>? Notes { get; set; } = new();
    }
}
