using Notka.Database.Data.Users;

namespace Notka.Database.Data.Notes
{
	public class Tag : BaseDatatable
	{
        public int UserId { get; set; }
        public User User { get; set; }
		public List<Note> Notes { get; set; } = new();
		public List<List> Lists { get; set; } = new();
		public List<Task> Tasks { get; set; } = new();
	}
}
