using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Views;

namespace NotkaMobile.ViewModels
{
	public partial class MainViewModel
	{
		[RelayCommand]
		private async Task GoToSettings()
		{
			await Shell.Current.GoToAsync(nameof(SettingsPage));
		}
	}
}
