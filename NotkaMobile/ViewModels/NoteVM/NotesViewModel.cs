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

		public override async void GoToAddPage()
		{
			await Shell.Current.GoToAsync(nameof(NewNotePage));
		}

		public override async void OnItemSelected(NoteForView item)	//async void might wrap awaited async Task method
		{
			if (item == null)
			{
				return;
			}
			await Shell.Current.GoToAsync($"{nameof(NoteDetailsPage)}?{nameof(NoteDetailsViewModel.ItemId)}={item.Id}");
		}
		[RelayCommand]
		private async Task Delete(NoteForView note)
		{
			await DataStore.DeleteItemAsync(note.Id);
			// This will pop the current page off the navigation stack
			//await Shell.Current.GoToAsync("..");
			await ExecuteLoadItemsCommand();
		}
		[RelayCommand]
		private async Task LoadMoreItems()
		{
			if (DataStore.PageParameters.HasNext)
			{
				DataStore.Params.PageNumber++;
				Console.WriteLine("Notes page number: {0}", DataStore.Params.PageNumber);
				var items = await DataStore.GetItemsAsync(true);
				foreach (var item in items)
				{
					Items.Add(item);
				}
				Console.WriteLine("Notes items count = {0}", Items.Count);
			}
		}
	}
}
