using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes.Note;
using System.Diagnostics;

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
		[RelayCommand]
		private async Task LoadMoreItems()
		{
			try
			{
				await Task.Delay(10);	//this prevents strange ObservableCollection synchronization error
				if (DataStore.PageParameters.HasNext && Items.Count > 0)
				{					
					DataStore.Params.PageNumber++;
					Debug.WriteLine("Notes page number: {0}", DataStore.Params.PageNumber);
					var items = await DataStore.GetItemsAsync(true);
					foreach (var item in items)
					{
						Items.Add(item);
					}
					Debug.WriteLine("Notes items count = {0}", Items.Count);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}
	}
}
