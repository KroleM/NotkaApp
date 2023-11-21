using Notka.Database.Data.General;
using Notka.Database.Data.Notes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notka.Database.Data.Users
{
	public class User : BaseDatatable
	{
		[Required]
		public string Nick { get; set; }
		[Required]
		public string PasswordHash { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		[Column(TypeName = "DATE")]
		public DateTime? BirthDate { get; set; }
		public string? Email { get; set; }
		public Login? Login { get; set; }
		public List<Picture> Pictures { get; set; } = new();
		public List<Role> Roles { get; set; } = new();
        public List<Note> Notes { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
        public List<List> Lists { get; set; }
        public List<User> RequestSender { get; set; } = new();
		public List<User> RequestReceiver { get; set; } = new();
        public List<Request> Senders { get; set; } = new();
		public List<Request> Receivers { get; set; } = new();
	}
}
