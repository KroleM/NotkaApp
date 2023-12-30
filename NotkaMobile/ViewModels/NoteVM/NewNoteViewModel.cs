using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using System.Collections.ObjectModel;
using Task = System.Threading.Tasks.Task;

namespace NotkaMobile.ViewModels.NoteVM
{
	public partial class NewNoteViewModel : ANewViewModel<NoteForView>
	{

		public NewNoteViewModel(NoteDataStore dataStore) 
			: base("Nowa notatka", dataStore)
		{
			_tagDataStore = new TagDataStore();
			_tagDataStore.RefreshListFromService();
			Tags = _tagDataStore.items;
		}
		#region Fields & Properties
		private TagDataStore _tagDataStore;
		public List<TagForView> Tags { get; set; } = new();

		[ObservableProperty]
		ObservableCollection<TagForView> _selectedTags = new();

		[ObservableProperty]
		ObservableCollection<TagForView> _promptedTags = new();

		[ObservableProperty]
		string _noteTitle = string.Empty;

		[ObservableProperty]
		string _text = string.Empty;

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
					var tags = Tags.Where(t => t.Name.ToLower().StartsWith(_currentTag.ToLower())).ToList();
					foreach (var tag in tags)
					{
						PromptedTags.Add(tag);
					}
				}
				else
				{
					PromptedTags.Clear();
				}
				
				OnPropertyChanged(nameof(CurrentTag));
			}
		}
		#endregion
		public override NoteForView SetItem()
		{
			return new NoteForView
			{
				Id = 0,
				IsActive = true,
				Name = this.NoteTitle,
				Description = this.Text,
				CreatedDate = DateTime.Now,
				ModifiedDate = DateTime.Now,
				UserId = Preferences.Default.Get("userId", 0),
				TagsForView = SelectedTags,
			};
		}
		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(NoteTitle);
		}

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
	}
}
