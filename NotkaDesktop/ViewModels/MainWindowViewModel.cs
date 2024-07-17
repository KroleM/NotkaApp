using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using NotkaDesktop.Helpers;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;
using NotkaMobile.Services;
using System.Collections.ObjectModel;

namespace NotkaDesktop.ViewModels
{
	public partial class MainWindowViewModel : BaseViewModel
	{
		public EventHandler LoggedOut;

		#region DataStores
		private UserDataStore _userDataStore = new UserDataStore();
		private UserDataStore _userDataStoreForRole = new UserDataStore();
		private RoleDataStore _roleDataStore = new RoleDataStore();
		private FeedDataStore _feedDataStore = new FeedDataStore();
		private CurrencyDataStore _currencyDataStore = new CurrencyDataStore();
		private CountryDataStore _countryDataStore = new CountryDataStore();
		private StockExchangeDataStore _stockExchangeDataStore = new StockExchangeDataStore();
		private StockDataStore _stockDataStore = new StockDataStore();
		#endregion

		#region ViewModels
		private MainPageViewModel? _mainPageViewModel;
		private UsersViewModel? _usersViewModel;
		private UserEditViewModel? _userEditViewModel;
		private RolesViewModel? _rolesViewModel;
		private NewRoleViewModel? _newRoleViewModel;
		private RoleEditViewModel? _roleEditViewModel;
		private FeedsViewModel? _feedsViewModel;
		private NewFeedViewModel? _newFeedViewModel;
		private FeedEditViewModel? _feedEditViewModel;
		private CurrenciesViewModel? _currenciesViewModel;
		private NewCurrencyViewModel? _newCurrencyViewModel;
		private CurrencyEditViewModel? _currencyEditViewModel;
		private CountriesViewModel? _countriesViewModel;
		private NewCountryViewModel? _newCountryViewModel;
		private CountryEditViewModel? _countryEditViewModel;
		private StockExchangesViewModel? _stockExchangesViewModel;
		private StockExchangeEditViewModel? _stockExchangeEditViewModel;
		private NewStockExchangeViewModel? _newStockExchangeEditViewModel;
		private StocksViewModel? _stocksViewModel;
		private NewStockViewModel? _newStockViewModel;
		private StockEditViewModel? _stockEditViewModel;

		#endregion

		#region Fields & Properties

		private ReadOnlyCollection<CommandViewModel> _leftPanelCommands;
		public ReadOnlyCollection<CommandViewModel> LeftPanelCommands
		{
			get
			{
				if (_leftPanelCommands == null)
				{
					List<CommandViewModel> cmds = this.CreateLeftPanelCommands();
					_leftPanelCommands = new ReadOnlyCollection<CommandViewModel>(cmds);
				}
				return _leftPanelCommands;
			}
		}

		[ObservableProperty]
		private BaseViewModel _rightPanelViewModel;
		private BaseViewModel _previousRightPanelViewModel;
		private MainWindowView _previousRightPanelType;
		#endregion

		#region Constructor
		public MainWindowViewModel() 
		{
			_mainPageViewModel = new MainPageViewModel();
			RightPanelViewModel = _mainPageViewModel;
		}
		#endregion

		#region Commands
		[RelayCommand]
		private async Task Logout()
		{
			LoggedOut?.Invoke(this, EventArgs.Empty);
		}
		#endregion

		#region Public methods
		public void Destroy()
		{
			WeakReferenceMessenger.Default.UnregisterAll(this);
		}
		#endregion

		#region Private methods
		private List<CommandViewModel> CreateLeftPanelCommands()
		{
			//Here: Register for WeakReferenceMessenger
			WeakReferenceMessenger.Default.Register<ViewRequestMessage>(this, (r, m) => OpenView(m));

			return new List<CommandViewModel>
			{
				new CommandViewModel(
					"Strona główna",
					new RelayCommand(() => this.ShowMainPage())),
				new CommandViewModel(
					"Użytkownicy",
					new RelayCommand(() => this.ShowUsers())),
				new CommandViewModel(
					"Role",
					new RelayCommand(() => this.ShowRoles())),
				new CommandViewModel(
					"Aktualności",
					new RelayCommand(() => this.ShowFeed())),
				new CommandViewModel(
					"Waluty",
					new RelayCommand(() => this.ShowCurrencies())),
				new CommandViewModel(
					"Kraje",
					new RelayCommand(() => this.ShowCountries())),
				new CommandViewModel(
					"Giełdy",
					new RelayCommand(() => this.ShowStockExchanges())),
				new CommandViewModel(
					"Spółki",
					new RelayCommand(() => this.ShowStocks())),
			};
		}

		private void OpenView(ViewRequestMessage viewRequestMessage)
		{
			switch (viewRequestMessage.Value)
			{
				case MainWindowView.Cancel:
					CancelView();
					break;
				case MainWindowView.BackAndRefresh:
					GoBackAndRefresh();
					break;
				case MainWindowView.Users:
					ShowUsers();
					break;
				case MainWindowView.EditUser:
					ShowEditUser();
					break;
				case MainWindowView.Roles:
					ShowRoles();
					break;
				case MainWindowView.NewRole:
					ShowNewRole();
					break;
				case MainWindowView.EditRole:
					ShowEditRole();
					break;
				case MainWindowView.Feeds:
					ShowFeed();
					break;
				case MainWindowView.NewFeed:
					ShowNewFeed();
					break;
				case MainWindowView.EditFeed:
					ShowEditFeed();
					break;
				case MainWindowView.Currencies: 
					ShowCurrencies();
					break;
				case MainWindowView.NewCurrency:
					ShowNewCurrency();
					break;
				case MainWindowView.EditCurrency:
					ShowEditCurrency();
					break;
				case MainWindowView.Countries:
					ShowCountries();
					break;
				case MainWindowView.NewCountry:
					ShowNewCountry();
					break;
				case MainWindowView.EditCountry:
					ShowEditCountry();
					break;
				case MainWindowView.StockExchanges:
					ShowStockExchanges();
					break;
				case MainWindowView.NewStockExchange:
					ShowNewStockExchange();
					break;
				case MainWindowView.EditStockExchange:
					ShowEditStockExchange();
					break;
				case MainWindowView.Stocks:
					ShowStocks();
					break;
				case MainWindowView.NewStock:
					ShowNewStock();
					break;
				case MainWindowView.EditStock:
					ShowEditStock();
					break;
			}
		}
		private void CancelView()
		{
			if (_previousRightPanelViewModel == null)
			{
				RightPanelViewModel = _mainPageViewModel;
			}
			else
			{
				RightPanelViewModel = _previousRightPanelViewModel;
			}
		}
		private void GoBackAndRefresh()
		{
			if (_previousRightPanelViewModel == null)
			{
				RightPanelViewModel = _mainPageViewModel;
			}
			else
			{
				WeakReferenceMessenger.Default.Send(new ViewRequestMessage(_previousRightPanelType));
			}
		}
		private void ShowMainPage()
		{
			RightPanelViewModel = _mainPageViewModel;
		}
		//User
		private void ShowUsers()
		{
			if (RightPanelViewModel == _usersViewModel) return;	//could cause bad behavior when using Delete

			_usersViewModel = new(_userDataStore, _roleDataStore);
			RightPanelViewModel = _usersViewModel;
		}
		private void ShowEditUser()
		{
			if (RightPanelViewModel == _userEditViewModel) return;	//cannot be invoked if UserEdit already invoked
			if (RightPanelViewModel is not UsersViewModel) return;	//can only be invoked from Users View (Model)

			_previousRightPanelType = MainWindowView.Users;
			_previousRightPanelViewModel = RightPanelViewModel;
			int itemId = (RightPanelViewModel as UsersViewModel).SelectedItem.Id; //int for loading particular item
			(RightPanelViewModel as UsersViewModel).SelectedItem = null;	//cancel the selection
			_userEditViewModel = new(_userDataStore, _roleDataStore, itemId);
			RightPanelViewModel = _userEditViewModel;	//change view
		}
		//Role
		private void ShowRoles()
		{
			_rolesViewModel = new(_roleDataStore, _userDataStoreForRole);
			RightPanelViewModel = _rolesViewModel;
		}
		private void ShowNewRole()
		{
			if (RightPanelViewModel == _newRoleViewModel) return;

			_previousRightPanelType = MainWindowView.Roles;
			_previousRightPanelViewModel = RightPanelViewModel;			
			_newRoleViewModel = new(_roleDataStore);
			RightPanelViewModel = _newRoleViewModel;
		}
		private void ShowEditRole()
		{
			if (RightPanelViewModel == _roleEditViewModel) return;
			if (RightPanelViewModel is not RolesViewModel) return;

			_previousRightPanelType = MainWindowView.Roles;
			_previousRightPanelViewModel = RightPanelViewModel;
			int itemId = (RightPanelViewModel as RolesViewModel).SelectedItem.Id;
			(RightPanelViewModel as RolesViewModel).SelectedItem = null;
			_roleEditViewModel = new(_roleDataStore, itemId);
			RightPanelViewModel = _roleEditViewModel;
		}
		//Feed
		private void ShowFeed()
		{
			_feedsViewModel = new(_feedDataStore);
			RightPanelViewModel = _feedsViewModel;
		}
		private void ShowNewFeed()
		{
			if (RightPanelViewModel == _newFeedViewModel) return;

			_previousRightPanelType = MainWindowView.Feeds;
			_previousRightPanelViewModel = RightPanelViewModel;
			_newFeedViewModel = new(_feedDataStore);
			RightPanelViewModel = _newFeedViewModel;
		}
		private void ShowEditFeed()
		{
			if (RightPanelViewModel == _feedEditViewModel) return;
			if (RightPanelViewModel is not FeedsViewModel) return;

			_previousRightPanelType = MainWindowView.Feeds;
			_previousRightPanelViewModel = RightPanelViewModel;
			int itemId = (RightPanelViewModel as FeedsViewModel).NewSelectedItem.Id;
			(RightPanelViewModel as FeedsViewModel).NewSelectedItem = null;
			_feedEditViewModel = new(_feedDataStore, itemId);
			RightPanelViewModel = _feedEditViewModel;
		}
		//Currency
		private void ShowCurrencies()
		{
			_currenciesViewModel = new(_currencyDataStore);
			RightPanelViewModel = _currenciesViewModel;
		}
		private void ShowNewCurrency()
		{
			if (RightPanelViewModel == _newCurrencyViewModel) return;

			_previousRightPanelType = MainWindowView.Currencies;
			_previousRightPanelViewModel = RightPanelViewModel;
			_newCurrencyViewModel = new(_currencyDataStore);
			RightPanelViewModel = _newCurrencyViewModel;
		}
		private void ShowEditCurrency()
		{
			if (RightPanelViewModel == _currencyEditViewModel) return;
			if (RightPanelViewModel is not CurrenciesViewModel) return;

			_previousRightPanelType = MainWindowView.Currencies;
			_previousRightPanelViewModel = RightPanelViewModel;
			int itemId = (RightPanelViewModel as CurrenciesViewModel).SelectedItem.Id;
			(RightPanelViewModel as CurrenciesViewModel).SelectedItem = null;
			_currencyEditViewModel = new(_currencyDataStore, itemId);
			RightPanelViewModel = _currencyEditViewModel;
		}
		//Country
		private void ShowCountries()
		{
			_countriesViewModel = new(_countryDataStore);
			RightPanelViewModel = _countriesViewModel;
		}
		private void ShowNewCountry()
		{
			if (RightPanelViewModel == _newCountryViewModel) return;

			_previousRightPanelType = MainWindowView.Countries;
			_previousRightPanelViewModel = RightPanelViewModel;
			_newCountryViewModel = new(_countryDataStore);
			RightPanelViewModel = _newCountryViewModel;
		}
		private void ShowEditCountry()
		{
			if (RightPanelViewModel == _countryEditViewModel) return;
			if (RightPanelViewModel is not CountriesViewModel) return;

			_previousRightPanelType = MainWindowView.Countries;
			_previousRightPanelViewModel = RightPanelViewModel;
			int itemId = (RightPanelViewModel as CountriesViewModel).SelectedItem.Id;
			(RightPanelViewModel as CountriesViewModel).SelectedItem = null;
			_countryEditViewModel = new(_countryDataStore, itemId);
			RightPanelViewModel = _countryEditViewModel;
		}
		//StockExchange
		private void ShowStockExchanges()
		{
			_stockExchangesViewModel = new(_stockExchangeDataStore);
			RightPanelViewModel = _stockExchangesViewModel;
		}
		private void ShowNewStockExchange()
		{
			if (RightPanelViewModel == _newStockExchangeEditViewModel) return;

			_previousRightPanelType = MainWindowView.StockExchanges;
			_previousRightPanelViewModel = RightPanelViewModel;
			_newStockExchangeEditViewModel = new(_stockExchangeDataStore, _countryDataStore);
			RightPanelViewModel = _newStockExchangeEditViewModel;
		}
		private void ShowEditStockExchange()
		{
			if (RightPanelViewModel == _countryEditViewModel) return;
			if (RightPanelViewModel is not StockExchangesViewModel) return;

			_previousRightPanelType = MainWindowView.StockExchanges;
			_previousRightPanelViewModel = RightPanelViewModel;
			int itemId = (RightPanelViewModel as StockExchangesViewModel).SelectedItem.Id;
			(RightPanelViewModel as StockExchangesViewModel).SelectedItem = null;
			_stockExchangeEditViewModel = new(_stockExchangeDataStore, _countryDataStore, itemId);
			RightPanelViewModel = _stockExchangeEditViewModel;
		}
		//Stock
		private void ShowStocks()
		{
			_stocksViewModel = new(_stockDataStore);
			RightPanelViewModel = _stocksViewModel;
		}		
		private void ShowNewStock()
		{
			if (RightPanelViewModel == _newStockViewModel) return;

			_previousRightPanelType = MainWindowView.Stocks;
			_previousRightPanelViewModel = RightPanelViewModel;
			_newStockViewModel = new(_stockDataStore, _stockExchangeDataStore, _currencyDataStore);
			RightPanelViewModel = _newStockViewModel;
		}
		private void ShowEditStock()
		{
			if (RightPanelViewModel == _stockEditViewModel) return;
			if (RightPanelViewModel is not StocksViewModel) return;

			_previousRightPanelType = MainWindowView.Stocks;
			_previousRightPanelViewModel = RightPanelViewModel;
			int itemId = (RightPanelViewModel as StocksViewModel).SelectedItem.Id;
			(RightPanelViewModel as StocksViewModel).SelectedItem = null;
			_stockEditViewModel = new(_stockDataStore, _stockExchangeDataStore, _currencyDataStore, itemId);
			RightPanelViewModel = _stockEditViewModel;
		}
		#endregion
	}
}
