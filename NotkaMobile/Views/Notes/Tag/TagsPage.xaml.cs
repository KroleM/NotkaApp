using NotkaMobile.ViewModels.TagVM;

namespace NotkaMobile.Views.Notes.Tag;

public partial class TagsPage : ContentPage
{
	public TagsPage(TagsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}