namespace NotkaAPI.ViewModels
{
	public class ReportUserForView
	{
		public int UserId { get; set; }
		public string Email { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public int NumberOfNotes { get; set; }
		public int NumberOfLists { get; set; }
		public int NumberOfTags { get; set; }
		//public int NumberOfStocksInPortfolio { get; set; }
	}
}
