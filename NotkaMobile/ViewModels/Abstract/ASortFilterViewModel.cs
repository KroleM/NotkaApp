using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Helpers;
using NotkaMobile.Services.Abstract;
using System.Collections.ObjectModel;

namespace NotkaMobile.ViewModels.Abstract
{
	public abstract partial class ASortFilterViewModel<T, U> : BaseViewModel
	{
		public ASortFilterViewModel(IDataStore<T, U> dataStore)
		{
			DataStore = dataStore;
			Title = "Sortowanie/Filtrowanie";
			ExecuteCommand = new AsyncRelayCommand(OnExecute);
			ClearChoicesCommand = new AsyncRelayCommand(OnClear);
			CreateSortItems();
		}
		[ObservableProperty]
		private SortClass? _selectedSortValue;
		protected IDataStore<T, U> DataStore { get; }
		public IAsyncRelayCommand ExecuteCommand { get; }
		public IAsyncRelayCommand ClearChoicesCommand { get; }

		public ObservableCollection<SortClass> SortItems { get; } = new();
		public ObservableCollection<T> FilterItems { get; } = new();

		protected abstract void CreateSortItems();
		public virtual async Task OnExecute()
		{
			await Shell.Current.GoToAsync("..");
		}
		public virtual Task OnClear()
		{
			return Task.CompletedTask;	//a way to declare "empty" Task method (it doesn't have to be overriden)
		}
	}
}
