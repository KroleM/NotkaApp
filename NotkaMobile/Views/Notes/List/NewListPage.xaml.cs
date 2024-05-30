using NotkaMobile.ViewModels.ListVM;

namespace NotkaMobile.Views.Notes.List;

public partial class NewListPage : ContentPage
{
	public NewListPage(NewListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}