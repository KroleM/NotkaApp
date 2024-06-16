using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using NotkaDesktop.Helpers;
using NotkaDesktop.Services.Abstract;
using System.Diagnostics;

namespace NotkaDesktop.ViewModels.Abstract
{
	public abstract class AEditViewModel<T, U> : BaseViewModel
	{
		protected AEditViewModel(string title, IDataStore<T, U> dataStore, int itemId)
		{
			Title = title;
			DataStore = dataStore;
			ItemId = itemId;
			CancelCommand = new AsyncRelayCommand(OnCancel);
			SaveCommand = new AsyncRelayCommand(OnSave, ValidateSave);
			this.PropertyChanged += (_, __) => SaveCommand.NotifyCanExecuteChanged();
		}
		public string SaveText { get; } = "Zapisz";
		public string CancelText { get; } = "Anuluj";
		protected IDataStore<T, U> DataStore { get; }
		public IAsyncRelayCommand SaveCommand { get; }
		public IAsyncRelayCommand CancelCommand { get; }
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
		protected async Task OnCancel()
		{
			WeakReferenceMessenger.Default.Send(new ViewRequestMessage(MainWindowView.Cancel));
		}
		protected async Task OnSave()
		{			
			try
			{
				await DataStore.UpdateItemAsync(SetItem());
				WeakReferenceMessenger.Default.Send(new ViewRequestMessage(MainWindowView.BackAndRefresh));
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}
	}
}
