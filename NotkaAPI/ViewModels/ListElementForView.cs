using NotkaAPI.Models;

namespace NotkaAPI.ViewModels
{
	public class ListElementForView : DictionaryTable
	{
		public int ListId { get; set; }
		public string? Answer { get; set; }
		public byte? Score { get; set; }
		public bool YesNo { get; set; } = false;
	}
}
