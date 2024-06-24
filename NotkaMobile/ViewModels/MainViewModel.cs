using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Views;
using NotkaMobile.Views.Feed;

namespace NotkaMobile.ViewModels
{
	public partial class MainViewModel
	{
		[RelayCommand]
		private async Task GoToSettings()
		{
			var userId = Preferences.Default.Get("userId", 0);
			await Shell.Current.GoToAsync($"{nameof(SettingsPage)}?{nameof(SettingsViewModel.ItemId)}={userId}");
		}

		[RelayCommand]
		private async Task GoToFeed()
		{
			var userId = Preferences.Default.Get("userId", 0);
			await Shell.Current.GoToAsync($"{nameof(FeedsPage)}");
		}
	}
}
