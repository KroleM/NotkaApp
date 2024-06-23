using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NotkaDesktop.ViewModels
{
	public partial class NewFeedViewModel : ANewViewModel<FeedForView, FeedParameters>
	{
		public NewFeedViewModel(FeedDataStore dataStore) 
			: base("Nowa aktualność", dataStore)
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

		#endregion
		#region Methods
		public override FeedForView SetItem()
		{
			return new FeedForView
			{
				Id = 0,
				IsActive = this.IsActive,
				Name = this.Name,
				Description = this.Description,
				CreatedDate = DateTimeOffset.Now,
				ModifiedDate = DateTimeOffset.Now,
				Picture = PhotoSource == null ? null : Photo
			};
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description);
		}
		#endregion
		#region Commands

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

		#endregion
	}
}
