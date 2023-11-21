namespace Notka.Database.Data.Users
{
	public class Role : DictionaryTable
	{
		public List<User> Users { get; set; } = new();
    }
}
