using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.ViewModels.Abstract;
using NotkaMobile.Services;
using System.Diagnostics;

namespace NotkaDesktop.ViewModels
{
	public partial class LoginViewModel : BaseViewModel
	{
		public EventHandler LoggedIn;
		public UserForView User { get; private set; } = new();
		[ObservableProperty]
		private string _email = string.Empty;
		[ObservableProperty]
		private string _password = string.Empty;
		//[ObservableProperty]
		//[NotifyPropertyChangedFor(nameof(Password))]
		//private bool _isHidden = true;
		private UserDataStore _loginDataStore;

		public LoginViewModel(UserDataStore loginDataStore)
		{
			Title = "Logowanie";
			_loginDataStore = loginDataStore;
		}

		[RelayCommand]
		private async Task Login()
		{
			try
			{
				IsBusy = true;
				if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
				{
					User = await _loginDataStore.LoginUser(Email, Password);
				}

				//Preferences.Default.Set("userEmail", Email);
				//Preferences.Default.Set("passwordHash", Password);
				//Preferences.Default.Set("userId", User.Id);
				ApplicationViewModel.s_userId = User.Id;
				Password = string.Empty;
				//await GoToMainPage();
				LoggedIn?.Invoke(this, EventArgs.Empty);
			}
			catch (ApiException ex)
			{
				if (ex.StatusCode == 404)
				{
					//await Shell.Current.DisplayAlert("Niepoprawne dane użytkownika", null, "OK");
					Password = string.Empty;
				}
				Debug.WriteLine($"Unable to get data: {ex.Message}");
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Unable to get data: {ex.Message}");
				//await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
