using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Services.Abstract;
using System.Diagnostics;

namespace NotkaMobile.ViewModels.Abstract
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
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
			// Add navigation to details page?
		}
		private async Task OnCancel()
		{
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}
	}
}
