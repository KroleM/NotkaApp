namespace NotkaAPI.Models.Users
{
	public class Role : DictionaryTable
	{
		public List<RoleUser> RoleUsers { get; set; } = new();
    }
}
