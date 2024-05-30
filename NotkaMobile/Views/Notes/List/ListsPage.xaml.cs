using NotkaMobile.Services;
using NotkaMobile.ViewModels.ListVM;

namespace NotkaMobile.Views.Notes.List;

public partial class ListsPage : ContentPage
{
	public ListsPage(ListsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}