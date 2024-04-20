using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotkaMobile.ViewModels
{
	public partial class MainViewModel
	{
		[RelayCommand]
		private async Task GoToSettings()
		{
			//await Shell.Current.GoToAsync(nameof(NoteSortFilterPage));
		}
	}
}
