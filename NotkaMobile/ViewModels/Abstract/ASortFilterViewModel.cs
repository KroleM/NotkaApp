using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Services.Abstract;
using System.Collections.ObjectModel;

namespace NotkaMobile.ViewModels.Abstract
{
	public class ASortFilterViewModel<T, U> : BaseViewModel
	{
		public ASortFilterViewModel(IDataStore<T, U> dataStore)
		{
			DataStore = dataStore;
			Title = "Filtrowanie/Sortowanie";
			ExecuteCommand = new AsyncRelayCommand(OnExecute);
		}

		protected IDataStore<T, U> DataStore { get; }
		public IAsyncRelayCommand ExecuteCommand { get; }

		public ObservableCollection<T> SortItems { get; }	//List? + niekoniecznie T
		public ObservableCollection<T> FilterItems { get; }


		public async Task OnExecute()
		{
			//kod ustawiający parametry w DataStore?
			await Shell.Current.GoToAsync("..");
		}
	}
}
