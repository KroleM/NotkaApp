using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using System.Collections.ObjectModel;

namespace NotkaMobile.ViewModels.NoteVM
{
	public partial class NoteEditViewModel : AEditViewModel<NoteForView, NoteParameters>
	{
		#region Constructor

		public NoteEditViewModel(NoteDataStore dataStore)
			: base("Edycja notatki", dataStore)
		{
			LoadTags();
		}

		#endregion
		#region Fields & Properties

		private TagDataStore _tagDataStore;
		private byte[] _bytesArray;
		public List<TagForView> Tags { get; set; } = new();
		[ObservableProperty]
		string _name;
		[ObservableProperty]
		string _description;
		[ObservableProperty]
		DateTimeOffset _createdDate;
		[ObservableProperty]
		DateTimeOffset _modifiedDate;
		[ObservableProperty]
		ImageSource? _photoSource;
		[ObservableProperty]
		ObservableCollection<TagForView> _selectedTags = new();

		[ObservableProperty]
		ObservableCollection<TagForView> _promptedTags = new();
		public Picture? Photo { get; set; }
		private string _currentTag = string.Empty;
		public string CurrentTag
		{
			get => _currentTag;
			set
			{
				if (EqualityComparer<string>.Default.Equals(_currentTag, value))
				{
					return;
				}
				OnPropertyChanging(nameof(CurrentTag));

				_currentTag = value;

				if (!string.IsNullOrWhiteSpace(_currentTag))
				{
					PromptedTags.Clear();
					var tags = Tags.Where(t => t.Name.ToLower().StartsWith(_currentTag.ToLower())).ToList().OrderByDescending(x => x.Name);
					foreach (var tag in tags)
					{
						PromptedTags.Add(tag);
					}
					//Updates `IsVisible` of ListView
					OnPropertyChanged(nameof(PromptedTags));
				}
				else
				{
					PromptedTags.Clear();
					//Updates `IsVisible` of ListView
					OnPropertyChanged(nameof(PromptedTags));
				}

				OnPropertyChanged(nameof(CurrentTag));
			}
		}

		#endregion
		#region Methods

		public override void LoadProperties()
		{
			Name = Item.Name;
			Description = Item.Description;
			CreatedDate = Item.CreatedDate;
			ModifiedDate = Item.ModifiedDate;
			PhotoSource = LoadPhoto(Item.Picture);

			SelectedTags.Clear();
			foreach (var tag in Item.TagsForView) 
			{
				SelectedTags.Add(tag);
			}
		}

		public override NoteForView SetItem()
		{
			Item.IsActive = true;
			Item.Name = this.Name;
			Item.Description = this.Description;
			Item.ModifiedDate = DateTime.Now;
			Item.TagsForView = SelectedTags;
			Item.Picture = PhotoSource == null ? null : Photo;

			return Item;
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(Name);
		}
		private ImageSource LoadPhoto(Picture? picture)
		{
			if (picture == null) return null;

			return ImageSource.FromStream(() => new MemoryStream(picture.BitPicture));
		}
		private async Task LoadTags()
		{
			_tagDataStore = new TagDataStore();
			await _tagDataStore.RefreshListFromService();
			Tags = _tagDataStore.items;
		}


		#endregion
		#region Commands

		[RelayCommand]
		void SelectTag(TagForView tag)
		{
			CurrentTag = tag.Name;
		}

		[RelayCommand]
		void AddSelectedTag()
		{
			if (string.IsNullOrWhiteSpace(CurrentTag))
			{
				return;
			}
			var tag = Tags.Find(tag => tag.Name == CurrentTag);
			if (tag == null)
			{
				tag = new TagForView
				{
					Id = 0,
					IsActive = true,
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.Now,
					Name = CurrentTag,
					Description = string.Empty,
					UserId = Preferences.Default.Get("userId", 0),
				};
			}
			SelectedTags.Add(tag);
			CurrentTag = string.Empty;
			PromptedTags.Clear();
		}

		[RelayCommand]
		void RemoveSelectedTag(TagForView tag)
		{
			if (SelectedTags.Contains(tag))
			{
				SelectedTags.Remove(tag);
			}
		}

		[RelayCommand]
		async System.Threading.Tasks.Task SelectPhoto()
		{
			FileResult photoFile = await MediaPicker.Default.PickPhotoAsync();

			if (photoFile == null)
				return;

			using var localFileStream = await photoFile.OpenReadAsync();
			//this shorter version works but only when localFileStream doesn't have "using" statement, which will cause memory leak
			//PhotoSource = ImageSource.FromStream(() => localFileStream);

			//MemoryStream, on the other hand, doesn't require disposing
			MemoryStream memory = new MemoryStream();
			localFileStream.CopyTo(memory);
			_bytesArray = memory.ToArray();
			PhotoSource = ImageSource.FromStream(() => new MemoryStream(_bytesArray));

			Photo = new Picture
			{
				Id = 0,
				IsActive = true,
				CreatedDate = DateTime.Now,
				ModifiedDate = DateTime.Now,
				IsProfile = false,
				UserId = Preferences.Default.Get("userId", 0),
				NoteId = 0,
				BitPicture = _bytesArray,
				PictureFormat = photoFile.ContentType
			};
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
