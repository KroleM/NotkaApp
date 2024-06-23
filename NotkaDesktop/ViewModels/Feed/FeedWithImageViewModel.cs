using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace NotkaDesktop.ViewModels
{
	public partial class FeedWithImageViewModel : ObservableObject
	{
		public int Id { get; set; }
		public bool IsActive { get; set; }
		public System.DateTimeOffset CreatedDate { get; set; }
		public System.DateTimeOffset ModifiedDate { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		[ObservableProperty]
		private ImageSource? _photoSource;
		//public ImageSource PhotoSource { get; set; }
	}
}
