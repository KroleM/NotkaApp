using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes;

namespace NotkaMobile.ViewModels.NoteVM
{
	public class NotesViewModel : AListViewModel<Note>
	{
		public NotesViewModel(NoteDataStore dataStore) 
			: base("Notatki", dataStore)
		{
		}

		public override async void GoToAddPage()
		{
			await Shell.Current.GoToAsync(nameof(NewNotePage));
		}

		public override async void OnItemSelected(Note item)
		{
			if (item == null)
			{
				return;
			}
			//await Shell.Current.GoToAsync($"{nameof(NoteDetailsPage)}?{nameof(NoteDetailsViewModel.ItemId)}={item.Id}");
		}
	}
}
