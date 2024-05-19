using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;

namespace NotkaMobile.ViewModels.UserVM
{
	public partial class UserEditViewModel : AEditViewModel<UserForView, UserParameters>
	{
		public UserEditViewModel(UserDataStore dataStore)
			: base("Dane użytkownika", dataStore)
		{
		}
		[ObservableProperty]
		string _email;
		[ObservableProperty]
		string _firstName;
		[ObservableProperty]
		string _lastName;
		[ObservableProperty]
		DateTimeOffset? _birthDate;
		[ObservableProperty]
		string _currentPassword;
		[ObservableProperty]
		string _newPassword;
		public override void LoadProperties()
		{
			Email = Item.Email;
			FirstName = Item.FirstName;
			LastName = Item.LastName;
			BirthDate = Item.BirthDate;
			CurrentPassword = string.Empty;
			NewPassword = string.Empty;
		}

		public override UserForView SetItem()
		{
			Item.IsActive = true;
			Item.Email = this.Email;
			Item.FirstName = this.FirstName;
			Item.LastName = this.LastName;
			Item.ModifiedDate = DateTimeOffset.Now;

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
