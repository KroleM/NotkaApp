using CommunityToolkit.Mvvm.ComponentModel;

namespace NotkaMobile.ViewModels.Abstract
{
    public abstract partial class BaseViewModel : ObservableObject
    {
		[ObservableProperty]
		bool isBusy = false;

		[ObservableProperty]
		string title = string.Empty;
	}
}
