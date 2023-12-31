﻿using CommunityToolkit.Mvvm.ComponentModel;
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
		public User User { get; private set; } = new();
        [ObservableProperty]
		private string _email = string.Empty;
		[ObservableProperty]
		private string _password = string.Empty;
		[ObservableProperty]
		private bool _isHidden = true;
		private LoginDataStore _loginDataStore;
		private IConnectivity _connectivity;

		public LoginViewModel(LoginDataStore loginDataStore, IConnectivity connectivity) 
		{
			Title = "Logowanie";
			_loginDataStore = loginDataStore;
			_connectivity = connectivity;
		}

		[RelayCommand]
		private async Task Appearing()
		{
			if (!Preferences.Default.ContainsKey("userEmail")) return;
			if (!Preferences.Default.ContainsKey("passwordHash")) return;

			string userEmail = Preferences.Default.Get("userEmail", string.Empty);
			string passwordHash = Preferences.Default.Get("passwordHash", string.Empty);

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
				if (!string.IsNullOrWhiteSpace(userEmail) && !string.IsNullOrWhiteSpace(passwordHash))
				{
					User = await _loginDataStore.LoginUser(userEmail, passwordHash);
				}

				Preferences.Default.Set("userId", User.Id);
				await GoToMainPage();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
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

				Preferences.Default.Set("userEmail", Email);
				Preferences.Default.Set("passwordHash", Password);
				Preferences.Default.Set("userId", User.Id);
				await GoToMainPage();
			}
			catch (ApiException ex)
			{
				if (ex.StatusCode == 404)
				{
					await Shell.Current.DisplayAlert("Niepoprawne dane użytkownika", null, "OK");
					Password = string.Empty;
				}
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
	}
}
