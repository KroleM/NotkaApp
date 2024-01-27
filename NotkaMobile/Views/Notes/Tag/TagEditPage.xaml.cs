using NotkaMobile.ViewModels.TagVM;

namespace NotkaMobile.Views.Notes.Tag;

public partial class TagEditPage : ContentPage
{
	public TagEditPage(TagEditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}