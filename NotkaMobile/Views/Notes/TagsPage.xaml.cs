using NotkaMobile.ViewModels.TagVM;

namespace NotkaMobile.Views.Notes;

public partial class TagsPage : ContentPage
{
	public TagsPage(TagsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}