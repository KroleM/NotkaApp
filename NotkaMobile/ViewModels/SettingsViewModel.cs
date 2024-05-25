using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.Services.Abstract;
using NotkaMobile.ViewModels.Abstract;

namespace NotkaMobile.ViewModels
{
	public partial class SettingsViewModel : AItemDetailsViewModel<UserForView, UserParameters>
	{
		public SettingsViewModel(UserDataStore dataStore) 
			: base(dataStore)
		{
			Title = "Mój profil";
		}

		[ObservableProperty]
		string _email;
		[ObservableProperty]
		string _firstName;
		[ObservableProperty]
		string _lastName;

		public override void LoadProperties(UserForView item)
		{
			Email = item.Email;
			FirstName = item.FirstName;
			LastName = item.LastName;
		}
		[RelayCommand]
		private async Task Logout()
		{
			Preferences.Default.Remove("userId");
			Preferences.Default.Remove("userEmail");
			Preferences.Default.Remove("passwordHash");
			await Shell.Current.GoToAsync("//Login");
		}
	}
}
