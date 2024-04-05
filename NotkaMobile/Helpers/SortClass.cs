namespace NotkaMobile.Helpers
{
	public class SortClass
	{
        public Enum SortEnum { get; set; }
        public string DisplayName { get; set; }

		public SortClass(Enum sortEnum, string displayName) 
		{
			SortEnum = sortEnum;
			DisplayName = displayName;
		}

		public override string ToString()
		{
			return DisplayName;
		}
	}
}
