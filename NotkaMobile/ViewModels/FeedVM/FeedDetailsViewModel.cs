using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.Services.Abstract;
using NotkaMobile.ViewModels.Abstract;

namespace NotkaMobile.ViewModels.FeedVM
{
	public partial class FeedDetailsViewModel : AItemDetailsViewModel<FeedForView, FeedParameters>
	{
		public FeedDetailsViewModel(FeedDataStore dataStore) 
			: base(dataStore)
		{
		}
		[ObservableProperty]
		string _name;
		[ObservableProperty]
		string _description;
		[ObservableProperty]
		ImageSource? _photoSource;

		public override void LoadProperties(FeedForView item)
		{
			Name = item.Name;
			Description = item.Description;
			PhotoSource = LoadPhoto(item.Picture);
		}
		private ImageSource LoadPhoto(Picture? picture)
		{
			if (picture == null || picture.BitPicture == null)
				return null;

			return ImageSource.FromStream(() => new MemoryStream(picture.BitPicture));
		}
	}
}
