using NotkaMobile.Services.Abstract;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NotkaMobile.ViewModels.Abstract
{
	public abstract class AEditViewModel<T> : BaseViewModel
	{
		protected AEditViewModel(IDataStore<T> dataStore)
		{
			DataStore = dataStore;
			CancelCommand = new Command(OnCancel);
			DeleteCommand = new Command(OnDelete);
			SaveCommand = new Command(OnSave, ValidateSave);
		}
		protected IDataStore<T> DataStore { get; }
		public Command SaveCommand { get; }
		public Command DeleteCommand { get; }
		public Command CancelCommand { get; }
		public T Item { get; set; }
		private int _itemId;
		public int ItemId
		{
			get { return _itemId; }
			set
			{
				_itemId = value;
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
		public abstract void LoadProperties(T item);
		public abstract T SetItem();
		public abstract bool ValidateSave();
		protected async void OnCancel()   //virtual?
		{
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}
		protected async void OnDelete()   
		{
			await DataStore.DeleteItemAsync(_itemId);
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");	//??
		}
		protected async void OnSave()
		{
			await DataStore.UpdateItemAsync(SetItem());
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
			// Add navigation to details page?
		}
	}
}
