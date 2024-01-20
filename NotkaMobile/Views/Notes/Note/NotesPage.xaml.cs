using NotkaMobile.ViewModels.NoteVM;

namespace NotkaMobile.Views.Notes.Note;

public partial class NotesPage : ContentPage
{
	public NotesPage(NotesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}