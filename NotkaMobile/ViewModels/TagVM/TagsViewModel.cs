using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes.Tag;
using System.Diagnostics;
using TagForView = NotkaMobile.Service.Reference.TagForView;

namespace NotkaMobile.ViewModels.TagVM
{
	public partial class TagsViewModel : AListViewModel<TagForView, TagParameters>
	{
		public TagsViewModel(TagDataStore dataStore) 
			: base("Tagi", dataStore)
		{
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
		//[RelayCommand]
		//private async Task LoadMoreItems()
		//{
		//	try
		//	{
		//		await Task.Delay(10);
		//		if (DataStore.PageParameters.HasNext && Items.Count > 0)
		//		{
		//			DataStore.Params.PageNumber++;
		//			Debug.WriteLine("Tags page number: {0}", DataStore.Params.PageNumber);
		//			var items = await DataStore.GetItemsAsync(true);
		//			foreach (var item in items)
		//			{
		//				Items.Add(item);
		//			}
		//			Debug.WriteLine("Tags items count = {0}", Items.Count);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		Debug.WriteLine(ex);
		//	}
		//}
	}
}
