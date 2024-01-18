using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Services.Abstract;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NotkaMobile.ViewModels.Abstract
{
	public abstract partial class AListViewModel<T> : BaseViewModel
	{
		protected AListViewModel(string title, IDataStore<T> dataStore)
		{
			Title = title;
			DataStore = dataStore;
			Items = new ObservableCollection<T>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
			ItemTapped = new Command<T>(OnItemSelected);
			AddItemCommand = new Command(OnAddItem);
		}

		//public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();
		private T? _selectedItem;
		protected IDataStore<T> DataStore { get; }
		public ObservableCollection<T> Items { get; }
		public Command LoadItemsCommand { get; }
		public Command AddItemCommand { get; }
		public Command<T> ItemTapped { get; }

		protected async Task ExecuteLoadItemsCommand()
		{
			IsBusy = true;
			try
			{
				Items.Clear();
				var items = await DataStore.GetItemsAsync(true);
				foreach (var item in items)
				{
					Items.Add(item);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
		public void OnAppearing()
		{
			IsBusy = true;
			SelectedItem = default(T);
		}

		[RelayCommand]
		void Appearing()
		{
			IsBusy = true;
		}

		public T? SelectedItem
		{
			get => _selectedItem;
			set
			{
				SetProperty(ref _selectedItem, value);
				OnItemSelected(value);
			}
		}
		public abstract void GoToAddPage();
		public async void OnAddItem(object obj)
		{
			GoToAddPage();
		}

		public async virtual void OnItemSelected(T item)
		{
			if (item == null)
				return;
		}
	}
}
