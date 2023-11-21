using Notka.Database.Data.Users;

namespace Notka.Database.Data.Notes
{
    public enum ListType2
    {
        OneTime, Repeated, Investment 
    }
	public class List : ANote
	{
        public List<ListElement> ListElements { get; set; } = new();
        // To może być enum ??
        public ListType2 ListType { get; set; }
    }
}
