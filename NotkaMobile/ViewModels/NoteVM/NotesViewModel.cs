using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes;

namespace NotkaMobile.ViewModels.NoteVM
{
	public partial class NotesViewModel : AListViewModel<NoteForView>
	{
		public NotesViewModel(NoteDataStore dataStore) 
			: base("Notatki", dataStore)
		{
		}

		public override async void GoToAddPage()
		{
			await Shell.Current.GoToAsync(nameof(NewNotePage));
		}

		public override async void OnItemSelected(NoteForView item)
		{
			if (item == null)
			{
				return;
			}
			await Shell.Current.GoToAsync($"{nameof(NoteDetailsPage)}?{nameof(NoteDetailsViewModel.ItemId)}={item.Id}");
		}
		[RelayCommand]
		private async System.Threading.Tasks.Task Delete(NoteForView note)
		{
			await DataStore.DeleteItemAsync(note.Id);
			// This will pop the current page off the navigation stack
			//await Shell.Current.GoToAsync("..");
			await ExecuteLoadItemsCommand();
		}
	}
}
