﻿using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Services.Abstract;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NotkaMobile.ViewModels.Abstract
{
	public abstract partial class AListViewModel<T> : BaseViewModel
	{
		//public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();
		public IDataStore<T> DataStore { get; }
		private T? _selectedItem;
		public ObservableCollection<T> Items { get; }
		public Command LoadItemsCommand { get; }
		public Command AddItemCommand { get; }
		public Command<T> ItemTapped { get; }

		public AListViewModel(string title, IDataStore<T> dataStore)
		{
			Title = title;
			DataStore = dataStore;
			Items = new ObservableCollection<T>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
			ItemTapped = new Command<T>(OnItemSelected);
			AddItemCommand = new Command(OnAddItem);
		}

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
