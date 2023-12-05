using NotkaMobile.ViewModels.NoteVM;

namespace NotkaMobile.Views.Notes;

public partial class NotesPage : ContentPage
{
	public NotesPage(NotesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}