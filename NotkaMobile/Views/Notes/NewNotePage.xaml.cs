using NotkaMobile.ViewModels.NoteVM;

namespace NotkaMobile.Views.Notes;

public partial class NewNotePage : ContentPage
{
	public NewNotePage(NewNoteViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}