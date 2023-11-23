using Microsoft.EntityFrameworkCore;
using NotkaAPI.Models.Users;

namespace NotkaAPI.Models.Notes
{
	[Index(nameof(Name), IsUnique = true)]
	public class Tag : DictionaryTable
	{
        public int UserId { get; set; }
        public User User { get; set; }
		public List<NoteTag> NoteTags { get; set; } = new();
		public List<ListTag> ListTags { get; set; } = new();
		public List<TaskTag> TaskTags { get; set; } = new();
	}
}
