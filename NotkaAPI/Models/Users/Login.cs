using System.ComponentModel.DataAnnotations.Schema;

namespace Notka.Database.Data.Users
{
	public class Login : BaseDatatable
	{
        public int UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public User User { get; set; }
        public bool Logged { get; set; }
        public int DeviceId { get; set; }
    }
}
