using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Services.Abstract;
using System.Diagnostics;

namespace NotkaMobile.ViewModels.Abstract
{
	//QueryProperty: first argument specifies the name of property that will receive the data;
	//second argument specifies parameter id
	[QueryProperty(nameof(ItemId), nameof(ItemId))]
	public abstract class AItemDetailsViewModel<T, U> : BaseViewModel
	{
		protected AItemDetailsViewModel(IDataStore<T, U> dataStore)
		{
			DataStore = dataStore;
			CancelCommand = new AsyncRelayCommand(OnCancel);
			DeleteCommand = new AsyncRelayCommand(OnDelete);
			EditCommand = new AsyncRelayCommand(OnEdit);
		}
		protected IDataStore<T, U> DataStore { get; }
		public IAsyncRelayCommand DeleteCommand { get; }
		public IAsyncRelayCommand CancelCommand { get; }
		public IAsyncRelayCommand EditCommand { get; }
		public abstract void LoadProperties(T item);
		private async Task OnDelete()
		{
			await DataStore.DeleteItemAsync(itemId);
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}
		private async Task OnCancel()
		{
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}
		protected async virtual Task OnEdit()	//FIXME abstract?
		{
			//await Shell.Current.GoToAsync($"{nameof(NoteEditPage)}?{nameof(NoteEditViewModel.ItemId)}={ItemId}");
		}

		private int itemId;
		public int ItemId
		{
			get
			{
				return itemId;
			}
			set
			{
				itemId = value;
				LoadItemId(value);
			}
		}

		private async void LoadItemId(int itemId)
		{
			try
			{
				var item = await DataStore.GetItemAsync(itemId);
				LoadProperties(item);
			}
			catch (Exception)
			{
				Debug.WriteLine("Failed to load Item");
			}
		}
	}
}
