﻿using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;

namespace NotkaMobile.ViewModels.UserVM
{
	public partial class UserPasswordEditViewModel : AEditViewModel<UserForView, UserParameters>
	{
		public UserPasswordEditViewModel(UserDataStore dataStore)
			: base("Zmiana hasła", dataStore)
		{
		}
		[ObservableProperty]
		string _currentPassword;
		[ObservableProperty]
		string _newPassword;
		[ObservableProperty]
		private bool _isHidden = true;
		public override void LoadProperties()
		{
			CurrentPassword = string.Empty;
			NewPassword = string.Empty;
		}

		public override UserForView SetItem()
		{
			Item.IsActive = true;
			Item.PasswordHash = this.NewPassword;
			Item.ModifiedDate = DateTimeOffset.Now;
			Preferences.Default.Set("passwordHash", NewPassword);

			return Item;
		}

		public override bool ValidateSave()
		{
			if (!string.IsNullOrEmpty(CurrentPassword))
			{
				if (CurrentPassword != Preferences.Default.Get("passwordHash", ""))
					return false;
				if (NewPassword.Length > 2)
					return true;
			}
			return false;
		}
	}
}
