using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes.Tag;
using TagForView = NotkaMobile.Service.Reference.TagForView;

namespace NotkaMobile.ViewModels.TagVM
{
	public partial class TagsViewModel : AListViewModel<TagForView, TagParameters>
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
		private async Task Delete(TagForView tag)
		{
			await DataStore.DeleteItemAsync(tag.Id);
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
				Console.WriteLine("Tags page number: {0}", DataStore.Params.PageNumber);
				var items = await DataStore.GetItemsAsync(true);
				foreach (var item in items)
				{
					Items.Add(item);
				}
				Console.WriteLine("Tags items count = {0}", Items.Count);
			}
		}
	}
}
