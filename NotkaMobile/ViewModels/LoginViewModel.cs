using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Helpers;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.User;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Messaging;

namespace NotkaMobile.ViewModels
{
	public partial class LoginViewModel : BaseViewModel
	{
		public UserForView User { get; private set; } = new();
        [ObservableProperty]
		private string _email = string.Empty;
		[ObservableProperty]
		private string _password = string.Empty;
		[ObservableProperty]
		private bool _isHidden = true;
		private UserDataStore _loginDataStore;
		private IConnectivity _connectivity;

		public LoginViewModel(UserDataStore loginDataStore, IConnectivity connectivity) 
		{
			Title = "Logowanie";
			_loginDataStore = loginDataStore;
			_connectivity = connectivity;
		}

		[RelayCommand]
		private async Task Appearing()
		{
			if (!Preferences.Default.ContainsKey("userEmail")) return;
			if (!Preferences.Default.ContainsKey("password")) return;

			string userEmail = Preferences.Default.Get("userEmail", string.Empty);
			string password = Preferences.Default.Get("password", string.Empty);

			if (IsBusy) 
				return;

			try
			{
				if (_connectivity.NetworkAccess != NetworkAccess.Internet)
				{
					await Shell.Current.DisplayAlert("Brak połączenia!",
						$"Sprawdź połączenie z internetem i spróbuj ponownie.", "OK");
					return;
				}

				IsBusy = true;
				if (!string.IsNullOrWhiteSpace(userEmail) && !string.IsNullOrWhiteSpace(password))
				{
					User = await _loginDataStore.LoginUser(userEmail, password);
				}
				if (User != null)
				{
					Preferences.Default.Set("userId", User.Id);
					Preferences.Default.Set("role", GetRole(User));
					await GoToMainPage();
				}
				else
				{
					Preferences.Default.Set("userEmail", string.Empty);
					Preferences.Default.Set("password", string.Empty);
					Preferences.Default.Set("userId", string.Empty);
					Preferences.Default.Set("role", string.Empty);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Appearing login error: {0}", ex.ToString());
			}
			finally
			{
				IsBusy = false;
				//IsRefreshing = false;
			}
		}
		[RelayCommand]
		private async Task GoToMainPage()
		{
			//await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
			await Shell.Current.GoToAsync($"//FeedPage");
		}
		[RelayCommand]
		private async Task GoToRegisterPage()
		{
			await Shell.Current.GoToAsync(nameof(NewUserPage));
		}
		[RelayCommand]
		private async Task Login()
		{
			if (IsBusy)
				return;

			try
			{
				if (_connectivity.NetworkAccess != NetworkAccess.Internet)
				{
					await Shell.Current.DisplayAlert("Brak połączenia!",
						"Sprawdź połączenie z internetem i spróbuj ponownie.", "OK");
					return;
				}

				IsBusy = true;
				if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
				{
					User = await _loginDataStore.LoginUser(Email, Password);
				}

				if (!User.RolesForView.Any(r => r.Id == 4 || r.Id == 5))
				{
					await Shell.Current.DisplayAlert("Niepoprawne dane użytkownika", 
						"Brak odpowiednich uprawnień", "OK");
					return;
				}

				Preferences.Default.Set("userEmail", Email);
				Preferences.Default.Set("password", Password);
				Preferences.Default.Set("userId", User.Id);
				Preferences.Default.Set("role", GetRole(User));
				Password = string.Empty;
				WeakReferenceMessenger.Default.Send(User);
				await GoToMainPage();
			}
			catch (ApiException ex)
			{
				if (ex.StatusCode == 404)
				{
					await Shell.Current.DisplayAlert("Niepoprawne dane użytkownika", null, "OK");
					Password = string.Empty;
				}
				Debug.WriteLine($"Unable to get data: {ex.Message}");
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Unable to get data: {ex.Message}");
				await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
			}
			finally
			{
				IsBusy = false;
				//IsRefreshing = false;
			}
		}

		private string GetRole(UserForView userForView)
		{
			string role = string.Empty;
			if (userForView != null)
			{
				if (userForView.RolesForView.Any(r => r.Id == 5))
				{
					role = UserRoles.Premium.ToString();
				}
				else if (userForView.RolesForView.Any(r => r.Id == 4))
				{
					role = UserRoles.Basic.ToString();
				}
			}
			return role;
		}
	}
}
