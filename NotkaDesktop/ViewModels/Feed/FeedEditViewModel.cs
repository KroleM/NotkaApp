using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;
using NotkaDesktop.Helpers;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;

namespace NotkaDesktop.ViewModels
{
	public partial class FeedEditViewModel : AEditViewModel<FeedForView, FeedParameters>
	{
		public FeedEditViewModel(FeedDataStore dataStore, int itemId)
			: base("Edycja aktualności", dataStore, itemId)
		{
		}
		#region Fields & Properties
		private byte[] _bytesArray = Array.Empty<byte>();
		public Picture? Photo { get; set; }

		[ObservableProperty]
		string _name = string.Empty;

		[ObservableProperty]
		string _description = string.Empty;

		[ObservableProperty]
		bool _isActive = true;

		[ObservableProperty]
		ImageSource? _photoSource;

		[ObservableProperty]
		int _pictureMaxWidth = 400;

		public override void LoadProperties()
		{
			Name = Item.Name;
			Description = Item.Description;
			IsActive = Item.IsActive;
			if (Item.Picture != null) { _bytesArray = Item.Picture.BitPicture; }
			PhotoSource = LoadPhoto(Item.Picture);
		}

		public override FeedForView SetItem()
		{
			Item.Name = this.Name;
			Item.Description = this.Description;
			Item.IsActive = this.IsActive;
			Item.ModifiedDate = DateTimeOffset.Now;
			Item.Picture = PhotoSource == null ? null : Photo;

			return Item;
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description);
		}
		#endregion

		private ImageSource? LoadPhoto(Picture? picture)
		{
			if (picture == null || picture.BitPicture == null || picture.BitPicture.Length == 0) return null;

			return Helpers.Helpers.LoadPhoto(picture.BitPicture);
		}

		[RelayCommand]
		async Task SelectPhoto(object obj)
		{
			var pictureFilePath = (string)obj;

			using (var localFileStream = File.Open(pictureFilePath, FileMode.Open))
			{
				MemoryStream memory = new MemoryStream();
				localFileStream.CopyTo(memory);
				_bytesArray = memory.ToArray();

				Photo = new Picture
				{
					Id = 0,
					IsActive = true,
					CreatedDate = DateTimeOffset.Now,
					ModifiedDate = DateTimeOffset.Now,
					IsProfile = false,
					UserId = ApplicationViewModel.s_userId,
					//NoteId = 0, //w tym wypadku NoteId musi być null
					BitPicture = _bytesArray,
					PictureFormat = System.IO.Path.GetExtension(pictureFilePath)
				};
			}
			PhotoSource = new BitmapImage(new Uri(pictureFilePath));
			PictureMaxWidth = (int)PhotoSource.Width;
		}

		[RelayCommand]
		void RemovePhoto()
		{
			_bytesArray = Array.Empty<byte>();
			PhotoSource = null;
		}
	}
}
