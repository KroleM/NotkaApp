﻿using ApiSharedClasses.QueryParameters.Abstract;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Services.Abstract;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NotkaMobile.ViewModels.Abstract
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
			ItemTapped = new AsyncRelayCommand<T>(OnItemSelected);
			AddItemCommand = new AsyncRelayCommand(OnAddItem);
			SortFilterCommand = new AsyncRelayCommand(OnSortFilterSelected);
		}

		//public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();
		private T? _selectedItem;
		protected IDataStore<T, U> DataStore { get; }
		public ObservableCollection<T> Items { get; }
		public IAsyncRelayCommand LoadItemsCommand { get; }
		public IAsyncRelayCommand AddItemCommand { get; }
		public IAsyncRelayCommand ItemTapped { get; }
		public IAsyncRelayCommand SortFilterCommand { get; }

		protected async Task ExecuteLoadItemsCommand()
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
			//await ExecuteLoadItemsCommand(); //Nie, bo to też zmienia isBusy na true i GetNotes() wywołuje się 2 razy
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

		public T? SelectedItem
		{
			get => _selectedItem;
			set
			{
				SetProperty(ref _selectedItem, value);
				OnItemSelected(value);
			}
		}
		public abstract Task GoToAddPage();
		public async Task OnAddItem()
		{
			await GoToAddPage();
		}

		public virtual Task OnItemSelected(T? item)
		{
			return Task.CompletedTask;
		}

		public virtual Task OnSortFilterSelected()
		{
			return Task.CompletedTask;
		}
	}
}
