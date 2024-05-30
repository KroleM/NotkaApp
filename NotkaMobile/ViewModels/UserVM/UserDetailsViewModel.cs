using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.User;

namespace NotkaMobile.ViewModels.UserVM
{
	public partial class UserDetailsViewModel : AItemDetailsViewModel<UserForView, UserParameters>
	{
		public UserDetailsViewModel(UserDataStore dataStore)
			: base(dataStore)
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
		public override void LoadProperties(UserForView item)
		{
			Email = item.Email;
			FirstName = item.FirstName;
			LastName = item.LastName;
			BirthDate = item.BirthDate;
		}
		protected async override Task OnEdit()
		{
			await Shell.Current.GoToAsync($"{nameof(UserEditPage)}?{nameof(UserEditViewModel.ItemId)}={ItemId}");
		}

		[RelayCommand]
		private async Task GoToPasswordChange()
		{
			await Shell.Current.GoToAsync($"{nameof(UserPasswordChangePage)}?{nameof(UserPasswordEditViewModel.ItemId)}={ItemId}");
		}
	}
}
