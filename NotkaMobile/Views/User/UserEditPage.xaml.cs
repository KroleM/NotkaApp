using NotkaMobile.ViewModels.UserVM;

namespace NotkaMobile.Views.User;

public partial class UserEditPage : ContentPage
{
	public UserEditPage(UserEditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}