using NotkaMobile.Services.Abstract;

namespace NotkaMobile.ViewModels.Abstract
{
	public abstract class ANewViewModel<T> : BaseViewModel
	{
		public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();
		public ANewViewModel(string title)
		{
			Title = title;
			SaveCommand = new Command(OnSave, ValidateSave);
			CancelCommand = new Command(OnCancel);
			this.PropertyChanged +=
				(_, __) => SaveCommand.ChangeCanExecute();
		}
		public abstract bool ValidateSave();
		public Command SaveCommand { get; }
		public Command CancelCommand { get; }
		private async void OnCancel()
		{
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}
		public abstract T SetItem();
		private async void OnSave()
		{
			await DataStore.AddItemAsync(SetItem());
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}
	}
}
