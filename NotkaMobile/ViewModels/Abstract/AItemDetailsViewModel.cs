using NotkaMobile.Services.Abstract;
using System.Diagnostics;

namespace NotkaMobile.ViewModels.Abstract
{
	[QueryProperty(nameof(ItemId), nameof(ItemId))]
	public abstract class AItemDetailsViewModel<T> : BaseViewModel
	{
		protected AItemDetailsViewModel(IDataStore<T> dataStore)
		{
			DataStore = dataStore;
			CancelCommand = new Command(OnCancel);
			DeleteCommand = new Command(OnDelete);
		}

		//public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();
		public IDataStore<T> DataStore { get; }
		public Command DeleteCommand { get; }
		public Command CancelCommand { get; }
		public Command EditCommand { get; }
		public abstract void LoadProperties(T item);
		private async void OnDelete()	//FIXME przerobić na Task i dodać atrybut [RelayCommand]?
		{
			await DataStore.DeleteItemAsync(itemId);
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}
		private async void OnCancel()   //FIXME przerobić na Task i dodać atrybut [RelayCommand]?
		{
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}
		private async void OnEdit()
		{

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

		public async void LoadItemId(int itemId)
		{
			try
			{
				var item = await DataStore.GetItemAsync(itemId);
				LoadProperties(item);
			}
			catch (Exception)
			{
				Debug.WriteLine("Failed to Load Item");
			}
		}
	}
}
