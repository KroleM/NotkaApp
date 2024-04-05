using Microsoft.Maui.Controls;
using NotkaMobile.ViewModels.NoteVM;
using System.Text.RegularExpressions;

namespace NotkaMobile.Views.Notes.Note;

public partial class NoteSortFilterPage : ContentPage
{
	public NoteSortFilterPage(NoteSortFilterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}