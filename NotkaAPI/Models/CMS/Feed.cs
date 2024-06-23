using NotkaAPI.Models.General;

namespace NotkaAPI.Models.CMS
{
	public class Feed : DictionaryTable
	{
		public int? PictureId { get; set; }
		public Picture? Picture { get; set; } = new();
	}
}
