using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;
using System.Collections.ObjectModel;

namespace NotkaDesktop.ViewModels
{
	public partial class StockExchangeEditViewModel : AEditViewModel<StockExchangeForView, StockExchangeParameters>
	{
		public StockExchangeEditViewModel(StockExchangeDataStore dataStore, CountryDataStore countryDataStore, int itemId)
			: base("Edycja giełdy", dataStore, itemId)
		{
			LoadCountries(countryDataStore);
		}
		#region Fields & Properties
		private CountryDataStore _countryDataStore;

		public ObservableCollection<CountryForView> Countries { get; set; } = new();

		[ObservableProperty]
		CountryForView? _selectedCountry;

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
		public override void LoadProperties()
		{
			Name = Item.Name;
			Description = Item.Description;
			IsActive = Item.IsActive;
			ShortName = Item.ShortName;
		}

		public override StockExchangeForView SetItem()
		{
			Item.IsActive = this.IsActive;
			Item.Name = this.Name;
			Item.Description = this.Description;
			Item.ModifiedDate = DateTimeOffset.Now;
			Item.ShortName = this.ShortName;
			Item.CountryId = SelectedCountry.Id;
			Item.CountryShortName = SelectedCountry.ShortName;

			return Item;
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
			foreach (var country in _countryDataStore.Items)
			{
				Countries.Add(country);
			}
			//Item is loaded in the constructor, so the operation may not end before this method (should be changed...)
			await Task.Delay(100);
			SelectedCountry = Countries.SingleOrDefault(c => c.Id == Item.CountryId);
		}
		#endregion
	}
}
