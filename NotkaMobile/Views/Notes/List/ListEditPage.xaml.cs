using NotkaMobile.ViewModels.ListVM;

namespace NotkaMobile.Views.Notes.List;

public partial class ListEditPage : ContentPage
{
	public ListEditPage(ListEditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}