using NotkaMobile.ViewModels.UserVM;

namespace NotkaMobile.Views.User;

public partial class UserPasswordChangePage : ContentPage
{
	public UserPasswordChangePage(UserPasswordEditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}