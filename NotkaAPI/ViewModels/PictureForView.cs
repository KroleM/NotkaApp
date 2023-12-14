using NotkaAPI.Models;

namespace NotkaAPI.ViewModels
{
	public class PictureForView : DictionaryTable
	{
		public byte[] BitPicture { get; set; }
		public string PictureFormat { get; set; }
	}
}
