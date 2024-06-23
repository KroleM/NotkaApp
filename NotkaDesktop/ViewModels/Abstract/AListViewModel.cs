using ApiSharedClasses.QueryParameters.Abstract;
using CommunityToolkit.Mvvm.Input;
using NotkaDesktop.Services.Abstract;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NotkaDesktop.ViewModels.Abstract
{
	public abstract partial class AListViewModel<T, U> : BaseViewModel 
		where U : AGetParameters
	{
		protected AListViewModel(string title, IDataStore<T, U> dataStore)
		{
			Title = title;
			DataStore = dataStore;
			Items = new ObservableCollection<T>();
			LoadItemsCommand = new AsyncRelayCommand(ExecuteLoadItemsCommand);
			AddItemCommand = new AsyncRelayCommand(OnAddItem);
			EditItemCommand = new AsyncRelayCommand(OnEditItem, ValidateEditOrDelete);
			DeleteItemCommand = new AsyncRelayCommand(OnDeleteItem, ValidateEditOrDelete);
			SortFilterCommand = new AsyncRelayCommand(OnSortFilterSelected);
			this.PropertyChanged += (_, __) => EditItemCommand.NotifyCanExecuteChanged();
			this.PropertyChanged += (_, __) => DeleteItemCommand.NotifyCanExecuteChanged();
			ExecuteLoadItemsCommand();
		}
		public string AddText { get; } = "Dodaj";
		public string EditText { get; } = "Edytuj";
		public string DeleteText { get; } = "Usuń";

		private T? _selectedItem;
		protected IDataStore<T, U> DataStore { get; }
		public ObservableCollection<T> Items { get; }
		public IAsyncRelayCommand LoadItemsCommand { get; }
		public IAsyncRelayCommand AddItemCommand { get; }
		public IAsyncRelayCommand EditItemCommand { get; }
		public IAsyncRelayCommand DeleteItemCommand { get; }
		public IAsyncRelayCommand SortFilterCommand { get; }
		public T? SelectedItem
		{
			get => _selectedItem;
			set
			{
				SetProperty(ref _selectedItem, value);
			}
		}

		public async Task ExecuteLoadItemsCommand() //virtual?
		{
			IsBusy = true;
			try
			{
				Items.Clear();
				DataStore.Params.PageNumber = 1;
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
		private void Appearing()
		{
			IsBusy = true;
		}
		[RelayCommand]
		private void PerformSearch(string query)
		{
			DataStore.Params.SearchPhrase = query;
			IsBusy = true;
		}
		[RelayCommand]
		private void SearchTextChanged(string newText)
		{
			if (string.IsNullOrWhiteSpace(newText))
			{
				PerformSearch(string.Empty);
			}
			return;
		}
		[RelayCommand]
		private async Task LoadMoreItems()  //virtual?
		{
			try
			{
				await Task.Delay(10);   //this prevents strange ObservableCollection synchronization error
				if (DataStore.PageParameters.HasNext && Items.Count > 0)
				{
					DataStore.Params.PageNumber++;
					var items = await DataStore.GetItemsAsync(true);
					foreach (var item in items)
					{
						Items.Add(item);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}

		public abstract Task GoToAddPage();
		public abstract Task OnDeleteItem();
		public abstract Task OnEditItem();
		private async Task OnAddItem()
		{
			await GoToAddPage();
		}
		public virtual Task OnSortFilterSelected()
		{
			return Task.CompletedTask;
		}
		private bool ValidateEditOrDelete()
		{
			return SelectedItem != null;
		}
	}
}
