using NotkaMobile.ViewModels.TagVM;

namespace NotkaMobile.Views.Notes;

public partial class TagDetailsPage : ContentPage
{
	public TagDetailsPage(TagDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}