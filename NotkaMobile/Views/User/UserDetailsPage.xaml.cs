using NotkaMobile.ViewModels.UserVM;

namespace NotkaMobile.Views.User;

public partial class UserDetailsPage : ContentPage
{
	public UserDetailsPage(UserDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}