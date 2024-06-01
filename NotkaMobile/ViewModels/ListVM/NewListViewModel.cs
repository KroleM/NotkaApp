using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using System.Collections.ObjectModel;

namespace NotkaMobile.ViewModels.ListVM
{
	public partial class NewListViewModel : ANewViewModel<ListForView, ListParameters>
	{
		#region Constructor

		public NewListViewModel(ListDataStore dataStore, TagDataStore tagDataStore)
			: base("Nowa lista", dataStore)
		{
			LoadTags(tagDataStore);
		}

		#endregion
		#region Fields & Properties

		private TagDataStore _tagDataStore;
		private byte[] _bytesArray;
		public List<TagForView> Tags { get; set; } = new();

		[ObservableProperty]
		ObservableCollection<TagForView> _selectedTags = new();

		[ObservableProperty]
		ObservableCollection<TagForView> _promptedTags = new();

		[ObservableProperty]
		ObservableCollection<ListElementForView> _listElements = new();

		[ObservableProperty]
		string _listTitle = string.Empty;

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

		public override ListForView SetItem()
		{
			return new ListForView
			{
				Id = 0,
				IsActive = true,
				Name = this.ListTitle,
				Description = this.Text,
				CreatedDate = DateTimeOffset.Now,
				ModifiedDate = DateTimeOffset.Now,
				UserId = Preferences.Default.Get("userId", 0),
				TagsForView = SelectedTags,
				ListElementsForView = ListElements,
			};
		}
		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(ListTitle);
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
		void AddNewListElement()
		{
			var listElement = new ListElementForView
			{
				Id = 0,
				IsActive = true,
				CreatedDate = DateTimeOffset.Now,
				ModifiedDate = DateTimeOffset.Now,
				Name = "element" + ListElements.Count.ToString(),
				Description = string.Empty,
				ListId = 0, //???
			};
			ListElements.Add(listElement);
		}

		[RelayCommand]
		void RemoveListElement(ListElementForView listElement)
		{
			ListElements.Remove(listElement);
		}

		#endregion
	}
}
