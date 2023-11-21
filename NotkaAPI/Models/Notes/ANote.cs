using Notka.Database.Data.General;
using Notka.Database.Data.Users;

namespace Notka.Database.Data.Notes
{
	public abstract class ANote : DictionaryTable
	{
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Tag> Tags { get; set; } = new();
        public List<Picture> Pictures { get; set; } = new();
    }
}
