using CommunityToolkit.Mvvm.ComponentModel;

namespace NotkaMobile.ViewModels.Abstract
{
    public abstract partial class BaseViewModel : ObservableObject
    {
		[ObservableProperty]
		bool _isBusy = false;

		[ObservableProperty]
		string _title = string.Empty;
	}
}
