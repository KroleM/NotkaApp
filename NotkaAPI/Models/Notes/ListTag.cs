using System.ComponentModel.DataAnnotations.Schema;

namespace NotkaAPI.Models.Notes
{
	public class ListTag : BaseDatatable
	{
		public int ListId { get; set; }
		[ForeignKey(nameof(ListId))]
		public List? List { get; set; }
		public int TagId { get; set; }
		[ForeignKey(nameof(TagId))]
		public Tag? Tag { get; set; }
		// Uwaga: w razie problemów z kaskadowym kasowaniem w BD należy zmienić opcję w pliku migracji: z Cascade na NoAction
	}
}
