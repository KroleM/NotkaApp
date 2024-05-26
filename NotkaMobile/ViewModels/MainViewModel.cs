using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Views;

namespace NotkaMobile.ViewModels
{
	public partial class MainViewModel
	{
		[RelayCommand]
		private async Task GoToSettings()
		{
			var userId = Preferences.Default.Get("userId", 0);
			//await Shell.Current.GoToAsync(nameof(SettingsPage));
			await Shell.Current.GoToAsync($"{nameof(SettingsPage)}?{nameof(SettingsViewModel.ItemId)}={userId}");
		}
	}
}
