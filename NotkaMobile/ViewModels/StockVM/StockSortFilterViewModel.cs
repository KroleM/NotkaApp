using ApiSharedClasses.QueryParameters;
using ApiSharedClasses.SortValues;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Helpers;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;

namespace NotkaMobile.ViewModels.StockVM
{
	public partial class StockSortFilterViewModel : ASortFilterViewModel<StockForView, StockParameters>
	{
		public StockSortFilterViewModel(StockDataStore dataStore, StockExchangeDataStore stockExchangeDataStore, CurrencyDataStore currencyDataStore) 
			: base("Sortowanie/Filtrowanie", dataStore)
		{
			LoadStockExchanges(stockExchangeDataStore);
			LoadSelectedStockExchange();

			LoadCurrencies(currencyDataStore);
			LoadSelectedCurrency();

			LoadSelectedSortValue();
		}

		private StockExchangeDataStore _stockExchangeDataStore;
		private CurrencyDataStore _currencyDataStore;

		public List<StockExchangeForView> StockExchanges { get; set; } = new();
		public List<CurrencyForView> Currencies {  get; set; } = new();

		[ObservableProperty]
		private StockExchangeForView _selectedStockExchange;

		[ObservableProperty]
		private CurrencyForView _selectedCurrency;

		protected override void CreateSortItems()
		{
			SortItems.Clear();
			SortItems.Add(new SortClass(StockSortValue.TickerFromAtoZ, "Ticker od A do Z"));
			SortItems.Add(new SortClass(StockSortValue.TickerFromZtoA, "Ticker od Z do A"));
			SortItems.Add(new SortClass(StockSortValue.NameFromAtoZ, "Nazwa od A do Z"));
			SortItems.Add(new SortClass(StockSortValue.NameFromZtoA, "Nazwa od Z do A"));
		}
		public override async Task OnExecute()
		{
			DataStore.Params.SortOrder = SelectedSortValue?.SortEnum.ToString() ?? string.Empty;
			DataStore.Params.StockExchangeId = SelectedStockExchange.Id;
			DataStore.Params.CurrencyId = SelectedCurrency.Id;

			await base.OnExecute();
		}
		public override async Task OnClear()
		{
			CreateSortItems();
			SelectedSortValue = null;
			SelectedStockExchange = StockExchanges[0];
			SelectedCurrency = Currencies[0];
		}

		/// <summary>
		/// Loads initial value of SelectedSortValue from DataStore. The delay allows for correct loading of SelectedItem in CollectionView
		/// </summary>
		/// <returns></returns>
		private async Task LoadSelectedSortValue()
		{
			//FIXME think about moving this method to ASortFilterViewModel (as virtual)
			await Task.Delay(100);

			SelectedSortValue = SortItems.LastOrDefault();
			if (!string.IsNullOrWhiteSpace(DataStore.Params.SortOrder))
			{
				NoteSortValue localEnum = new();
				Enum.TryParse(DataStore.Params.SortOrder, out localEnum);
				SelectedSortValue = SortItems.FirstOrDefault(x => (NoteSortValue)x.SortEnum == localEnum);
			}
			//OnPropertyChanged(nameof(SelectedSortValue));
		}

		private async Task LoadStockExchanges(StockExchangeDataStore stockExchangeDataStore)
		{
			_stockExchangeDataStore = stockExchangeDataStore;
			_stockExchangeDataStore.Params.PageSize = 0;
			await _stockExchangeDataStore.RefreshListFromService();
			StockExchanges.Add(new StockExchangeForView
			{
				Id = 0,
				IsActive = true,
				Name = "Dowolna",
				ShortName = "---"
			});
			StockExchanges.AddRange(_stockExchangeDataStore.Items);
		}

		private void LoadSelectedStockExchange()
		{
			SelectedStockExchange = StockExchanges.FirstOrDefault(se => se.Id == DataStore.Params.StockExchangeId);
		}

		private async Task LoadCurrencies(CurrencyDataStore currencyDataStore)
		{
			_currencyDataStore = currencyDataStore;
			_currencyDataStore.Params.PageSize = 0;
			await _currencyDataStore.RefreshListFromService();
			Currencies.Add(new CurrencyForView
			{
				Id = 0,
				IsActive = true,
				Name = "Dowolna",
				ShortName = "---"
			});
			Currencies.AddRange(_currencyDataStore.Items);
		}

		private void LoadSelectedCurrency()
		{
			SelectedCurrency = Currencies.FirstOrDefault(c => c.Id == DataStore.Params.CurrencyId);
		}
	}
}
