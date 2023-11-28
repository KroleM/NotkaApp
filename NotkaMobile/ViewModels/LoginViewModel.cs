using CommunityToolkit.Mvvm.Input;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views;

namespace NotkaMobile.ViewModels
{
	public partial class LoginViewModel : BaseViewModel
	{

		[RelayCommand]
		async Task GoToMainPage()
		{
			await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
		}
	}
}
