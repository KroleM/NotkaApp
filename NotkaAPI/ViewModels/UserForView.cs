using NotkaAPI.Models;

namespace NotkaAPI.ViewModels
{
	public class UserForView : BaseDatatable
	{
		public string Email { get; set; }
		public string? PasswordHash { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public DateTime? BirthDate { get; set; }
	}
}
