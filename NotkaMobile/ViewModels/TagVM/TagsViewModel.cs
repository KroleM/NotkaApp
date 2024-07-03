using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes.Tag;

namespace NotkaMobile.ViewModels.TagVM
{
	public partial class TagsViewModel : AListViewModel<TagForView, TagParameters>
	{
		public TagsViewModel(TagDataStore dataStore) 
			: base("Tagi", dataStore)
		{
			DataStore.Params.PageSize = 12;
		}

		public override async Task GoToAddPage()
		{
			await Shell.Current.GoToAsync(nameof(NewTagPage));
		}

		public override async Task OnItemSelected(TagForView? item)
		{
			if (item == null)
			{
				return;
			}
			await Shell.Current.GoToAsync($"{nameof(TagDetailsPage)}?{nameof(TagDetailsViewModel.ItemId)}={item.Id}");
		}
		[RelayCommand]
		private async Task Delete(TagForView tag)
		{
			await DataStore.DeleteItemAsync(tag.Id);
			// This will pop the current page off the navigation stack
			//await Shell.Current.GoToAsync("..");
			await ExecuteLoadItemsCommand();
		}
	}
}
