using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes;
using TagForView = NotkaMobile.Service.Reference.TagForView;

namespace NotkaMobile.ViewModels.TagVM
{
	public partial class TagsViewModel : AListViewModel<TagForView>
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
			await Shell.Current.GoToAsync($"{nameof(TagDetailsPage)}?{nameof(TagDetailsViewModel.ItemId)}={item.Id}");
		}
		[RelayCommand]
		private async System.Threading.Tasks.Task Delete(TagForView tag)
		{
			await DataStore.DeleteItemAsync(tag.Id);
			// This will pop the current page off the navigation stack
			//await Shell.Current.GoToAsync("..");
			await ExecuteLoadItemsCommand();
		}
	}
}
