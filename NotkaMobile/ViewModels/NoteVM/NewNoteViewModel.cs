using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using System.Collections.ObjectModel;
using Task = System.Threading.Tasks.Task;

namespace NotkaMobile.ViewModels.NoteVM
{
	public partial class NewNoteViewModel : ANewViewModel<Note>
	{

		public NewNoteViewModel() 
			: base("Nowa notatka")
		{
			_tagDataStore = new TagDataStore();
			_tagDataStore.RefreshListFromService();
			Tags = _tagDataStore.items;
		}
		#region Fields & Properties
		private TagDataStore _tagDataStore;
		public List<Tag> Tags { get; set; } = new();

		[ObservableProperty]
		ObservableCollection<Tag> _selectedTags = new();

		[ObservableProperty]
		ObservableCollection<Tag> _promptedTags = new();

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
					var tags = Tags.Where(t => t.Name.StartsWith(_currentTag)).ToList();
					foreach (var tag in tags)
					{
						PromptedTags.Add(tag);
					}
				}

				OnPropertyChanged(nameof(CurrentTag));
			}
		}
		#endregion
		public override Note SetItem()
		{
			return new Note
			{
				Id = 0,
				IsActive = true,
				Name = this.NoteTitle,
				Description = this.Text,
			};
		}
		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(Title);
		}

		[RelayCommand]
		void SelectTag(Tag tag)
		{
			CurrentTag = tag.Name;
		}

		[RelayCommand]
		void AddTag()
		{
			if (string.IsNullOrWhiteSpace(CurrentTag))
			{
				return;
			}
			var tag = Tags.Find(tag => tag.Name == CurrentTag);
			if (tag == null)
			{
				tag = new Tag
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
		}

		[RelayCommand]
		void RemoveTag(Tag tag)
		{
			if (SelectedTags.Contains(tag))
			{
				SelectedTags.Remove(tag);
			}
		}

		[RelayCommand]
		async Task AddNote()
		{

		}
	}
}
