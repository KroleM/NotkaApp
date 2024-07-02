using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.ViewModels.StockVM;
using NotkaMobile.Views.Investments.Stock;
using System.Collections.ObjectModel;

namespace NotkaMobile.ViewModels.PortfolioVM
{
	public partial class PortfolioEditViewModel : AEditViewModel<PortfolioForView, PortfolioParameters>
	{
		//FIXME tryb dla użytkowników Premium?
		#region Constructor

		public PortfolioEditViewModel(PortfolioDataStore dataStore) 
			: base("Portfolio", dataStore)
		{
			ItemId = 0;	//allows to load portfolio
		}

		#endregion
		#region Fields & Properties

		[ObservableProperty]
		ObservableCollection<StockForView> _stocks = new();

		[ObservableProperty]
		StockForView? _selectedStock;	//czy potrzebny?

		#endregion
		#region Methods

		public override void LoadProperties()
		{
			Stocks.Clear();
			foreach (var stock in Item.StocksForView)
			{
				Stocks.Add(stock);
			}
		}

		public override PortfolioForView SetItem()
		{
			Item.StocksForView = Stocks;

			return Item;
		}

		public override bool ValidateSave()
		{
			return true;
		}

		#endregion
		#region Commands

		[RelayCommand]
		private async Task AddStock()
		{
			await Shell.Current.GoToAsync(nameof(StocksPage));
		}

		[RelayCommand]
		private async Task RemoveStock(StockForView stock)
		{
			Stocks.Remove(stock);
			await DataStore.UpdateItemAsync(SetItem());
		}

		[RelayCommand]
		private async Task SelectStock(StockForView stock)
		{
			if (stock == null)
			{
				return;
			}
			await Shell.Current.GoToAsync($"{nameof(StockDetailsPage)}?{nameof(StockDetailsViewModel.ItemId)}={stock.Id}");
		}

		#endregion
	}
}
