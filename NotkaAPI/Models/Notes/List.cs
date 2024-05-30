namespace NotkaAPI.Models.Notes
{
    public enum ListType
    {
        OneTime, Repeated, Investment 
    }
	public class List : ANote
	{
        public List<ListTag> ListTags { get; set; } = new();
		public List<ListElement> ListElements { get; set; } = new();
        //public ListType ListType { get; set; }
    }
}
