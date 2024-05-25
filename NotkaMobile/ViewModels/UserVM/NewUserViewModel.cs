using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using System.Diagnostics;

namespace NotkaMobile.ViewModels.UserVM
{
	public partial class NewUserViewModel : ANewViewModel<UserForView, UserParameters>
	{
		public NewUserViewModel(UserDataStore dataStore) 
			: base("Rejestracja", dataStore)
		{
		}

		[ObservableProperty]
		string _email;
		[ObservableProperty]
		string _firstName = string.Empty;
		[ObservableProperty]
		string _lastName = string.Empty;
		[ObservableProperty]
		DateTime? _birthDate;
		[ObservableProperty]
		string _password;
		[ObservableProperty]
		private bool _isHidden = true;
		public override UserForView SetItem()
		{
			return new UserForView
			{
				Id = 0,
				IsActive = true,
				CreatedDate = DateTimeOffset.Now,
				ModifiedDate = DateTimeOffset.Now,
				Email = _email,
				FirstName = _firstName,
				LastName = _lastName,
				BirthDate = _birthDate,
				PasswordHash = _password,
			};
		}

		public override bool ValidateSave()
		{
			bool isValid = true;
			if (!Email.Contains('@')) isValid = false;
			if (string.IsNullOrEmpty(Password) || Password?.Length < 3) isValid = false;
			return isValid;
		}

		//FIXME zamiast tej komendy mogłoby zostać po prostu użyte SaveCommand?
		[RelayCommand]
		private async Task Register()
		{
			if (IsBusy)
				return;

			try
			{
				IsBusy = true;
				if (ValidateSave())
				{
					await DataStore.AddItemAsync(SetItem());
					await Shell.Current.DisplayAlert("Gratulacje! Twoje konto zostało utworzone.", null, "OK");
					await Shell.Current.GoToAsync("..");
				}
				else
				{
					await Shell.Current.DisplayAlert("Wypełnij wszystkie dane.", null, "OK");
				}
			}
			catch (ApiException ex)
			{
				if (ex.StatusCode == 409)
				{
					await Shell.Current.DisplayAlert("Ten e-mail istnieje już w bazie.", null, "OK");
				}
				Debug.WriteLine($"Unable to get data: {ex.Message}");
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Błąd rejestracji: {ex.Message}");
				await Shell.Current.DisplayAlert("Błąd rejestracji!", ex.Message, "OK");
			}
			finally
			{
				IsBusy = false;
				//IsRefreshing = false;
			}
		}
	}
}
