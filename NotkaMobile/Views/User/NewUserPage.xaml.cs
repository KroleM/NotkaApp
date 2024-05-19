using NotkaMobile.ViewModels.UserVM;

namespace NotkaMobile.Views.User;

public partial class NewUserPage : ContentPage
{
	public NewUserPage(NewUserViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}