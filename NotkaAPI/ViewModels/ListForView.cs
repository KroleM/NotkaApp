using NotkaAPI.Models;
using NotkaAPI.Models.General;

namespace NotkaAPI.ViewModels
{
	public class ListForView : DictionaryTable
	{
		public int UserId { get; set; }
		public List<TagForView> TagsForView { get; set; } = new();  //FIXME new() może nie być konieczne
		public List<ListElementForView> ListElementsForView { get; set; } = new();  //FIXME new() może nie być konieczne
	}
}
