﻿using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using System.Collections.ObjectModel;

namespace NotkaMobile.ViewModels.ListVM
{
	public partial class ListEditViewModel : AEditViewModel<ListForView, ListParameters>
	{
		#region Constructor
		public ListEditViewModel(ListDataStore dataStore, TagDataStore tagDataStore)
			: base("Edycja listy", dataStore)
		{
			LoadTags(tagDataStore);
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
		ObservableCollection<TagForView> _selectedTags = new();

		[ObservableProperty]
		ObservableCollection<TagForView> _promptedTags = new();

		[ObservableProperty]
		ObservableCollection<ListElementForView> _listElements = new();

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

			SelectedTags.Clear();
			foreach (var tag in Item.TagsForView)
			{
				SelectedTags.Add(tag);
			}

			ListElements.Clear();
			foreach (var listElement in Item.ListElementsForView)
			{
				ListElements.Add(listElement);
			}
		}

		public override ListForView SetItem()
		{
			Item.IsActive = true;
			Item.Name = this.Name;
			Item.Description = this.Description;
			Item.ModifiedDate = DateTimeOffset.Now;
			Item.TagsForView = SelectedTags;
			Item.ListElementsForView = ListElements;

			return Item;
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(Name);
		}
		private async Task LoadTags(TagDataStore tagDataStore)
		{
			_tagDataStore = tagDataStore;
			//load all user's tags
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
