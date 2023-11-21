using System.ComponentModel.DataAnnotations.Schema;

namespace NotkaAPI.Models.Notes
{
	public class ListElement : DictionaryTable
	{
		public int ListId { get; set; }
		[ForeignKey(nameof(ListId))]
		public List List { get; set; }
        //dodać ocenę, komentarz/odpowiedź, notatkę?
        public string? Answer { get; set; }
        public byte? Score { get; set; }
        public bool YesNo { get; set; }
    }
}
