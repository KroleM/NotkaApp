using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;
using System.Collections.ObjectModel;

namespace NotkaDesktop.ViewModels
{
	public partial class StockEditViewModel : AEditViewModel<StockForView, StockParameters>
	{
		public StockEditViewModel(StockDataStore dataStore, StockExchangeDataStore stockExchangeDataStore, CurrencyDataStore currencyDataStore, int itemId) 
			: base("Nowa spółka", dataStore, itemId)
		{
			_stockExchangeDataStore = stockExchangeDataStore;
			_currencyDataStore = currencyDataStore;
		}

		#region Fields & Properties
		private StockExchangeDataStore _stockExchangeDataStore;
		private CurrencyDataStore _currencyDataStore;

		public ObservableCollection<StockExchangeForView> StockExchanges { get; set; } = new();
		public ObservableCollection<CurrencyForView> Currencies { get; set; } = new();

		[ObservableProperty]
		StockExchangeForView? _selectedStockExchange;

		[ObservableProperty]
		CurrencyForView? _selectedCurrency;

		[ObservableProperty]
		string _name = string.Empty;

		[ObservableProperty]
		string _description = string.Empty;

		[ObservableProperty]
		bool _isActive = true;

		[ObservableProperty]
		string _ticker = string.Empty;

		#endregion
		#region Methods
		public override void LoadProperties()
		{
			Name = Item.Name;
			Description = Item.Description;
			IsActive = Item.IsActive;
			Ticker = Item.Ticker;

			LoadStockExchanges();
			LoadCurrencies();
		}

		public override StockForView SetItem()
		{
			Item.IsActive = this.IsActive;
			Item.Name = this.Name;
			Item.Description = this.Description;
			Item.ModifiedDate = DateTimeOffset.Now;
			Item.Ticker = this.Ticker;
			Item.StockExchangeId = SelectedStockExchange.Id;
			Item.CurrencyId = SelectedCurrency.Id;

			return Item;
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(Name)
				&& !string.IsNullOrEmpty(Ticker)
				&& SelectedStockExchange != null
				&& SelectedCurrency != null;
		}

		private async Task LoadStockExchanges()
		{
			//_stockExchangeDataStore = stockExchangeDataStore;
			_stockExchangeDataStore.Params.PageSize = 0;
			await _stockExchangeDataStore.RefreshListFromService();
			foreach (var stockExchange in _stockExchangeDataStore.Items)
			{
				StockExchanges.Add(stockExchange);
			}
			//Item is loaded in the constructor, so the operation may not end before this method (should be changed...)
			//await Task.Delay(100);
			SelectedStockExchange = StockExchanges.SingleOrDefault(s => s.Id == Item.StockExchangeId);
		}

		private async Task LoadCurrencies()
		{
			//_stockExchangeDataStore = stockExchangeDataStore;
			_currencyDataStore.Params.PageSize = 0;
			await _currencyDataStore.RefreshListFromService();
			foreach (var currency in _currencyDataStore.Items)
			{
				Currencies.Add(currency);
			}
			//Item is loaded in the constructor, so the operation may not end before this method (should be changed...)
			//await Task.Delay(100);
			SelectedCurrency = Currencies.SingleOrDefault(c => c.Id == Item.CurrencyId);
		}
		#endregion
	}
}
