using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;

namespace NotkaDesktop.ViewModels
{
	public partial class NewStockExchangeViewModel : ANewViewModel<StockExchangeForView, StockExchangeParameters>
	{
		public NewStockExchangeViewModel(StockExchangeDataStore dataStore, CountryDataStore countryDataStore) 
			: base("Nowa giełda", dataStore)
		{
			LoadCountries(countryDataStore);
		}
		#region Fields & Properties
		private CountryDataStore _countryDataStore;

		public List<CountryForView> Countries { get; set; } = new();

		[ObservableProperty]
		CountryForView _selectedCountry;

		[ObservableProperty]
		string _name = string.Empty;

		[ObservableProperty]
		string _description = string.Empty;

		[ObservableProperty]
		bool _isActive = true;

		[ObservableProperty]
		string _shortName = string.Empty;

		#endregion
		#region Methods
		public override StockExchangeForView SetItem()
		{
			return new StockExchangeForView
			{
				Id = 0,
				IsActive = this.IsActive,
				Name = this.Name,
				Description = this.Description,
				CreatedDate = DateTimeOffset.Now,
				ModifiedDate = DateTimeOffset.Now,
				ShortName = this.ShortName,
				CountryId = SelectedCountry.Id,
				CountryShortName = SelectedCountry.ShortName,
			};
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(Name) && SelectedCountry != null;
		}

		private async Task LoadCountries(CountryDataStore countryDataStore)
		{
			_countryDataStore = countryDataStore;
			_countryDataStore.Params.PageSize = 0;
			await _countryDataStore.RefreshListFromService();
			Countries = _countryDataStore.Items;
		}
		#endregion
	}
}
