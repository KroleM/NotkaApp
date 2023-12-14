using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes;
using Tag = NotkaMobile.Service.Reference.Tag;

namespace NotkaMobile.ViewModels.TagVM
{
	public class TagsViewModel : AListViewModel<Tag>
	{
		public TagsViewModel(TagDataStore dataStore) 
			: base("Tagi", dataStore)
		{
		}

		public override async void GoToAddPage()
		{
			await Shell.Current.GoToAsync(nameof(NewTagPage));
		}

		public override async void OnItemSelected(Tag item)
		{
			if (item == null)
			{
				return;
			}
			//await Shell.Current.GoToAsync($"{nameof(NoteDetailsPage)}?{nameof(NoteDetailsViewModel.ItemId)}={item.Id}");
		}
	}
}
