using Microsoft.EntityFrameworkCore;
using NotkaAPI.Models.General;
using NotkaAPI.Models.Notes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotkaAPI.Models.Users
{
	[Index(nameof(Email), IsUnique = true)]
	public class User : BaseDatatable
	{
		[Required]
		public string Email { get; set; }
		[Required]
		public string PasswordHash { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		[Column(TypeName = "DATE")]
		public DateTime? BirthDate { get; set; }
		public string? Nick { get; set; }
		public Login? Login { get; set; }
		public List<RoleUser> RoleUsers { get; set; } = new();
		public List<Picture> Pictures { get; set; } = new();
        public List<Note> Notes { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
        public List<List> Lists { get; set; } = new();
		public List<User> RequestSender { get; set; } = new(); // ??
		public List<User> RequestReceiver { get; set; } = new(); // ??
        public List<Request> Senders { get; set; } = new();
		public List<Request> Receivers { get; set; } = new();
		//FIXME Are all these navigation lists necessary?
	}
}
