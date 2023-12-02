using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Networking;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views;
using System.Diagnostics;
using Task = System.Threading.Tasks.Task;

namespace NotkaMobile.ViewModels
{
	public partial class LoginViewModel : BaseViewModel
	{
		public User User { get; set; } = new();
        [ObservableProperty]
		private string _email = string.Empty;
		[ObservableProperty]
		private string _password = string.Empty;
		private LoginDataStore _loginDataStore;
		private IConnectivity _connectivity;
		private IGeolocation _geolocation;

		public LoginViewModel(LoginDataStore loginDataStore, IConnectivity connectivity, IGeolocation geolocation) 
		{
			Title = "Logowanie";
			_loginDataStore = loginDataStore;
			_connectivity = connectivity;
			_geolocation = geolocation;
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
						$"Sprawdź połączenie z internetem i spróbuj ponownie.", "OK");
					return;
				}

				IsBusy = true;
				if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
				{
					User = await _loginDataStore.LoginUser(Email, Password);
				}
				if (User.Id != 0)
				{
					await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Unable to get data: {ex.Message}");
				await Shell.Current.DisplayAlert("Błąd!", ex.Message, "OK");
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
			await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
		}
	}
}
