using NotkaDesktop.Service.Reference;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NotkaDesktop.Helpers
{
	public static class Helpers
	{
		/// <summary>
		/// Metoda typu rozszerzenie. Jako argument przyjmuje element, na którym została wywołana. 
		/// Jeżeli akcja wywoła się bez problemu, zwraca true, a jak nie to false i wypisuje błąd.
		/// </summary>
		/// <param name="serviceMethod"></param>
		/// <returns></returns>
		public async static Task<bool> HandleRequest(this Task serviceMethod)
		{
			try
			{
				await serviceMethod;
				return true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return false;
			}
		}

		public static ImageSource? LoadPhoto(byte[] imageData)
		{
			var imageSource = new BitmapImage();
			using (MemoryStream memoryStream = new MemoryStream(imageData))
			{

				memoryStream.Position = 0;
				imageSource.BeginInit();
				imageSource.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
				imageSource.CacheOption = BitmapCacheOption.OnLoad;
				imageSource.UriSource = null;
				imageSource.StreamSource = memoryStream;
				imageSource.EndInit();
			}
			imageSource.Freeze();

			return imageSource;
		}
	}
}
