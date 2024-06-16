using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using NotkaDesktop.Helpers;
using NotkaDesktop.Services.Abstract;
using System.Diagnostics;

namespace NotkaDesktop.ViewModels.Abstract
{
	public abstract class ANewViewModel<T, U> : BaseViewModel
	{
		protected ANewViewModel(string title, IDataStore<T, U> dataStore)
		{
			Title = title;
			DataStore = dataStore;
			SaveCommand = new AsyncRelayCommand(OnSave, ValidateSave);
			CancelCommand = new AsyncRelayCommand(OnCancel);
			this.PropertyChanged += (_, __) => SaveCommand.NotifyCanExecuteChanged(); //SaveCommand.ChangeCanExecute();
		}
		public string CreateText { get; } = "Utwórz";
		public string CancelText { get; } = "Anuluj";
		protected IDataStore<T, U> DataStore { get; }
		public IAsyncRelayCommand SaveCommand { get; }
		public IAsyncRelayCommand CancelCommand { get; }
		public abstract T SetItem();
		public abstract bool ValidateSave();
		protected async Task OnSave()
		{
			try
			{
				await DataStore.AddItemAsync(SetItem());
				WeakReferenceMessenger.Default.Send(new ViewRequestMessage(MainWindowView.BackAndRefresh));
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}			
		}
		private async Task OnCancel()
		{
			WeakReferenceMessenger.Default.Send(new ViewRequestMessage(MainWindowView.Cancel));
		}
	}
}
