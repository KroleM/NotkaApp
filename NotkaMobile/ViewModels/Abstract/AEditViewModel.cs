using NotkaMobile.Services.Abstract;
using System.Diagnostics;

namespace NotkaMobile.ViewModels.Abstract
{
	[QueryProperty(nameof(ItemId), nameof(ItemId))]
	public abstract class AEditViewModel<T> : BaseViewModel
	{
		protected AEditViewModel(string title, IDataStore<T> dataStore)
		{
			Title = title;
			DataStore = dataStore;
			CancelCommand = new Command(OnCancel);
			SaveCommand = new Command(OnSave, ValidateSave);
			this.PropertyChanged +=
				(_, __) => SaveCommand.ChangeCanExecute();
		}
		protected IDataStore<T> DataStore { get; }
		public Command SaveCommand { get; }
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
				Item = await DataStore.GetItemAsync(itemId);
				LoadProperties();
			}
			catch (Exception)
			{
				Debug.WriteLine("Failed to Load Item");
			}
		}
		public abstract void LoadProperties();
		public abstract T SetItem();
		public abstract bool ValidateSave();
		protected async void OnCancel()
		{
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}
		protected async void OnSave()
		{
			await DataStore.UpdateItemAsync(SetItem());
			await Shell.Current.GoToAsync("..");
		}
	}
}
