using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using System.Collections.ObjectModel;

namespace NotkaMobile.ViewModels.NoteVM
{
	public partial class NewNoteViewModel : ANewViewModel<NoteForView, NoteParameters>
	{
		#region Constructor

		public NewNoteViewModel(NoteDataStore dataStore, TagDataStore tagDataStore)
			: base("Nowa notatka", dataStore)
		{
			LoadTags(tagDataStore);
		}

		#endregion
		#region Fields & Properties

		private TagDataStore _tagDataStore;
		private byte[] _bytesArray;
		public List<TagForView> Tags { get; set; } = new();
		public Picture? Photo { get; set; } 

		[ObservableProperty]
		ObservableCollection<TagForView> _selectedTags = new();

		[ObservableProperty]
		ObservableCollection<TagForView> _promptedTags = new();

		[ObservableProperty]
		string _noteTitle = string.Empty;

		[ObservableProperty]
		string _text = string.Empty;

		[ObservableProperty]
		ImageSource? _photoSource;

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

		public override NoteForView SetItem()
		{
			return new NoteForView
			{
				Id = 0,
				IsActive = true,
				Name = this.NoteTitle,
				Description = this.Text,
				CreatedDate = DateTimeOffset.Now,   //DateTime.UtcNow,	//Now.ToUniversalTime(),
				ModifiedDate = DateTimeOffset.Now,
				UserId = Preferences.Default.Get("userId", 0),
				TagsForView = SelectedTags,
				Picture = PhotoSource == null ? null : Photo
			};
		}
		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(NoteTitle);
		}
		private async Task LoadTags(TagDataStore tagDataStore)
		{
			_tagDataStore = tagDataStore;
			_tagDataStore.Params.PageSize = 0;
			await _tagDataStore.RefreshListFromService();
			Tags = _tagDataStore.Items;
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
					CreatedDate = DateTimeOffset.Now,
					ModifiedDate = DateTimeOffset.Now,
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
		async Task SelectPhoto()
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
				CreatedDate = DateTimeOffset.Now,
				ModifiedDate = DateTimeOffset.Now,
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
