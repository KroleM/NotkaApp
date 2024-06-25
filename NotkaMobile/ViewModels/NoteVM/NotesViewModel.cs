using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes.Note;

namespace NotkaMobile.ViewModels.NoteVM
{
	public partial class NotesViewModel : AListViewModel<NoteForView, NoteParameters>
	{
		public NotesViewModel(NoteDataStore dataStore)
			: base("Notatki", dataStore)
		{
		}

		public override async Task GoToAddPage()
		{
			await Shell.Current.GoToAsync(nameof(NewNotePage));
		}

		public override async Task OnItemSelected(NoteForView? item)
		{
			if (item == null)
			{
				return;
			}
			await Shell.Current.GoToAsync($"{nameof(NoteDetailsPage)}?{nameof(NoteDetailsViewModel.ItemId)}={item.Id}");
		}
		public override async Task OnSortFilterSelected()
		{
			await Shell.Current.GoToAsync(nameof(NoteSortFilterPage));
		}

		[RelayCommand]
		private async Task Delete(NoteForView note)
		{
			await DataStore.DeleteItemAsync(note.Id);
			// This will pop the current page off the navigation stack
			//await Shell.Current.GoToAsync("..");
			await ExecuteLoadItemsCommand();
		}
	}
}
