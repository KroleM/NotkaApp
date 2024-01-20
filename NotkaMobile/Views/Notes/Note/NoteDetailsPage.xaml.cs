using NotkaMobile.ViewModels.NoteVM;

namespace NotkaMobile.Views.Notes.Note;

public partial class NoteDetailsPage : ContentPage
{
	public NoteDetailsPage(NoteDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}