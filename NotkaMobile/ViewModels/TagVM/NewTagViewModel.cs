﻿using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;

namespace NotkaMobile.ViewModels.TagVM
{
	public partial class NewTagViewModel : ANewViewModel<TagForView, TagParameters>
	{
		public NewTagViewModel(TagDataStore dataStore) 
			: base("Nowy tag", dataStore)
		{
		}

		[ObservableProperty]
		string _tagName = string.Empty;
		[ObservableProperty]
		string _description = string.Empty;
		public override TagForView SetItem()
		{
			return new TagForView
			{
				Id = 0,
				IsActive = true,
				Name = this.TagName,
				Description = this.Description,
				CreatedDate = DateTimeOffset.Now,
				ModifiedDate = DateTimeOffset.Now,
				UserId = Preferences.Default.Get("userId", 0),
			};
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(TagName) && TagName.Length < 60;
		}
	}
}
