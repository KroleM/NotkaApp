using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.ViewModels.PortfolioVM;
using NotkaMobile.Views.Investments.Portfolio;
using NotkaMobile.Views.Investments.Stock;

namespace NotkaMobile.ViewModels.StockVM
{
	public partial class StocksViewModel : AListViewModel<StockForView, StockParameters>
	{
		public StocksViewModel(StockDataStore dataStore, PortfolioDataStore portfolioDataStore) 
			: base("Spółki", dataStore)
		{
			LoadPortfolio(portfolioDataStore);
		}

		private PortfolioDataStore _portfolioDataStore;
		private PortfolioForView _portfolioForView;

		#region Commands
		[RelayCommand]
		private async Task AddStockToPortfolio(StockForView stock)
		{
			//? inject PortfolioDataStore?
			if (stock != null)
			{
				_portfolioForView.StocksForView.Add(stock);
				await _portfolioDataStore.UpdateItemInService(_portfolioForView);
				//await Shell.Current.GoToAsync($"..?{nameof(PortfolioEditViewModel.ItemId)}=0");
				await Shell.Current.GoToAsync($"{nameof(PortfolioEditPage)}?{nameof(PortfolioEditViewModel.ItemId)}=0");
			}
		}

		public override Task GoToAddPage()
		{
			return Task.CompletedTask;
		}

		public override async Task OnItemSelected(StockForView? item)
		{
			if (item == null)
			{
				return;
			}
			await Shell.Current.GoToAsync($"{nameof(StockDetailsPage)}?{nameof(StockDetailsViewModel.ItemId)}={item.Id}");
		}

		public override async Task OnSortFilterSelected()
		{
			await Shell.Current.GoToAsync(nameof(StockSortFilterPage));
		}

		private async Task LoadPortfolio(PortfolioDataStore portfolioDataStore)
		{
			_portfolioDataStore = portfolioDataStore;
			_portfolioForView = await _portfolioDataStore.GetItemAsync(0);	//gets user's portfolio
		}

		#endregion
	}
}
