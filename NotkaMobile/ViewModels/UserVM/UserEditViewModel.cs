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
			: base("Edycja danych", dataStore)
		{
		}
		[ObservableProperty]
		string _email;
		[ObservableProperty]
		string _firstName;
		[ObservableProperty]
		string _lastName;
		[ObservableProperty]
		DateTime? _birthDate;

		public override void LoadProperties()
		{
			Email = Item.Email;
			FirstName = Item.FirstName;
			LastName = Item.LastName;
			BirthDate = Item.BirthDate?.DateTime;
		}

		public override UserForView SetItem()
		{
			Item.IsActive = true;
			Item.Email = this.Email;
			Item.FirstName = this.FirstName;
			Item.LastName = this.LastName;
			Item.BirthDate = this.BirthDate;
			Item.ModifiedDate = DateTimeOffset.Now;

			return Item;
		}

		public override bool ValidateSave()
		{
			if (!string.IsNullOrEmpty(Email) && Email.Contains('@')) return true;

			return false;
		}
	}
}
