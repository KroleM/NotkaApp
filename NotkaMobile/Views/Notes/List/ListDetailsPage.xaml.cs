using NotkaMobile.ViewModels.ListVM;

namespace NotkaMobile.Views.Notes.List;

public partial class ListDetailsPage : ContentPage
{
	public ListDetailsPage(ListDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}