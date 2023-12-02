using System.ComponentModel.DataAnnotations.Schema;

namespace NotkaAPI.Models.Users
{
	public class Login : BaseDatatable
	{
        public int UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public User User { get; set; }
        public bool IsLogged { get; set; }
        public int DeviceId { get; set; }
    }
}
