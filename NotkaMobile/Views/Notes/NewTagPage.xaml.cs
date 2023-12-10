using NotkaMobile.ViewModels.TagVM;

namespace NotkaMobile.Views.Notes;

public partial class NewTagPage : ContentPage
{
	public NewTagPage(NewTagViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}