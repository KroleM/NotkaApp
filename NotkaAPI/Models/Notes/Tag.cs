using NotkaAPI.Models.Users;

namespace NotkaAPI.Models.Notes
{
	public class Tag : BaseDatatable
	{
        public int UserId { get; set; }
        public User User { get; set; }
		public List<NoteTag> NoteTags { get; set; } = new();
		public List<ListTag> ListTags { get; set; } = new();
		public List<TaskTag> TaskTags { get; set; } = new();
	}
}
