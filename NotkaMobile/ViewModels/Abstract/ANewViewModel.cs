using NotkaMobile.Services.Abstract;

namespace NotkaMobile.ViewModels.Abstract
{
	public abstract class ANewViewModel<T> : BaseViewModel
	{
		protected ANewViewModel(string title, IDataStore<T> dataStore)
		{
			Title = title;
			DataStore = dataStore;
			SaveCommand = new Command(OnSave, ValidateSave);
			CancelCommand = new Command(OnCancel);
			this.PropertyChanged +=
				(_, __) => SaveCommand.ChangeCanExecute();
		}
		protected IDataStore<T> DataStore { get; }
		public Command SaveCommand { get; }
		public Command CancelCommand { get; }
		public abstract T SetItem();
		public abstract bool ValidateSave();
		protected async void OnSave()
		{
			await DataStore.AddItemAsync(SetItem());
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
			// Add navigation to details page?
		}
		private async void OnCancel()
		{
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}
	}
}
