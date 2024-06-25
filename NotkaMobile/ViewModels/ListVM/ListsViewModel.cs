using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes.List;

namespace NotkaMobile.ViewModels.ListVM
{
	public partial class ListsViewModel : AListViewModel<ListForView, ListParameters>
	{
		public ListsViewModel(ListDataStore dataStore)
			: base("Listy", dataStore)
		{
		}

		public override async Task GoToAddPage()
		{
			await Shell.Current.GoToAsync(nameof(NewListPage));
		}
		public override async Task OnItemSelected(ListForView? item)
		{
			if (item == null)
			{
				return;
			}
			await Shell.Current.GoToAsync($"{nameof(ListDetailsPage)}?{nameof(ListDetailsViewModel.ItemId)}={item.Id}");
		}
		public override async Task OnSortFilterSelected()
		{
			await Shell.Current.GoToAsync(nameof(ListSortFilterPage));
		}

		[RelayCommand]
		private async Task Delete(ListForView item)
		{
			await DataStore.DeleteItemAsync(item.Id);
			await ExecuteLoadItemsCommand();
		}
	}
}
