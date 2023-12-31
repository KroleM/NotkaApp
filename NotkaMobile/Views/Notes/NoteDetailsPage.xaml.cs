using NotkaMobile.ViewModels.NoteVM;

namespace NotkaMobile.Views.Notes;

public partial class NoteDetailsPage : ContentPage
{
	public NoteDetailsPage(NoteDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}