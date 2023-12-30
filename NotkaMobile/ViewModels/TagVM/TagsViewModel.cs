using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes;
using TagForView = NotkaMobile.Service.Reference.TagForView;

namespace NotkaMobile.ViewModels.TagVM
{
	public class TagsViewModel : AListViewModel<TagForView>
	{
		public TagsViewModel(TagDataStore dataStore) 
			: base("Tagi", dataStore)
		{
		}

		public override async void GoToAddPage()
		{
			await Shell.Current.GoToAsync(nameof(NewTagPage));
		}

		public override async void OnItemSelected(TagForView item)
		{
			if (item == null)
			{
				return;
			}
			//await Shell.Current.GoToAsync($"{nameof(NoteDetailsPage)}?{nameof(NoteDetailsViewModel.ItemId)}={item.Id}");
		}
	}
}
