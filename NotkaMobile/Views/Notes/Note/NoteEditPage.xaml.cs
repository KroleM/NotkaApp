using NotkaMobile.ViewModels.NoteVM;

namespace NotkaMobile.Views.Notes.Note;

public partial class NoteEditPage : ContentPage
{
	public NoteEditPage(NoteEditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}