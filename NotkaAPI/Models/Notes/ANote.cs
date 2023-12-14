using NotkaAPI.Models.Users;

namespace NotkaAPI.Models.Notes
{
	public abstract class ANote : DictionaryTable
	{
        public int UserId { get; set; }
        //public User User { get; set; }
    }
}
