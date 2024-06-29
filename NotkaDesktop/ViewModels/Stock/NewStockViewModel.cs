using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;

namespace NotkaDesktop.ViewModels
{
	public partial class NewStockViewModel : ANewViewModel<StockForView, StockParameters>
	{
		public NewStockViewModel(StockDataStore dataStore, StockExchangeDataStore stockExchangeDataStore, CurrencyDataStore currencyDataStore) 
			: base("Nowa spółka", dataStore)
		{
			LoadStockExchanges(stockExchangeDataStore);
			LoadCurrencies(currencyDataStore);
		}

		#region Fields & Properties
		private StockExchangeDataStore _stockExchangeDataStore;
		private CurrencyDataStore _currencyDataStore;

		public List<StockExchangeForView> StockExchanges { get; set; } = new();
		public List<CurrencyForView> Currencies { get; set; } = new();

		[ObservableProperty]
		StockExchangeForView _selectedStockExchange;

		[ObservableProperty]
		CurrencyForView _selectedCurrency;

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
		public override StockForView SetItem()
		{
			return new StockForView
			{
				Id = 0,
				IsActive = this.IsActive,
				Name = this.Name,
				Description = this.Description,
				CreatedDate = DateTimeOffset.Now,
				ModifiedDate = DateTimeOffset.Now,
				Ticker = this.Ticker,
				StockExchangeId = SelectedStockExchange.Id,
				StockExchangeShortName = SelectedStockExchange.ShortName,
				CurrencyId = SelectedCurrency.Id,
				CurrencyShortName = SelectedCurrency.ShortName,
			};
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(Name)
				&& !string.IsNullOrEmpty(Ticker)
				&& SelectedStockExchange != null
				&& SelectedCurrency != null;
		}

		private async Task LoadStockExchanges(StockExchangeDataStore stockExchangeDataStore)
		{
			_stockExchangeDataStore = stockExchangeDataStore;
			_stockExchangeDataStore.Params.PageSize = 0;
			await _stockExchangeDataStore.RefreshListFromService();
			StockExchanges = _stockExchangeDataStore.Items;
		}

		private async Task LoadCurrencies(CurrencyDataStore currencyDataStore)
		{
			_currencyDataStore = currencyDataStore;
			_currencyDataStore.Params.PageSize = 0;
			await _currencyDataStore.RefreshListFromService();
			Currencies = _currencyDataStore.Items;
		}
		#endregion
	}
}
