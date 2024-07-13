using NotkaAPI.Models;

namespace NotkaAPI.ViewModels
{
	public class UserForView : BaseDatatable
	{
		public string Email { get; set; }
		public string? Password { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public DateTime? BirthDate { get; set; }
		public List<RoleForView> RolesForView { get; set; } = new();  //FIXME new() może nie być konieczne
	}
}
