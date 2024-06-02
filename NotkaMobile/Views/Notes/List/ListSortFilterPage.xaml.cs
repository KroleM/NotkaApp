using NotkaMobile.ViewModels.ListVM;

namespace NotkaMobile.Views.Notes.List;

public partial class ListSortFilterPage : ContentPage
{
	public ListSortFilterPage(ListSortFilterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}