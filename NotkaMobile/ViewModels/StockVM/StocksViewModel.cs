using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Investments.Stock;

namespace NotkaMobile.ViewModels.StockVM
{
	public partial class StocksViewModel : AListViewModel<StockForView, StockParameters>
	{
		public StocksViewModel(StockDataStore dataStore) 
			: base("Spółki", dataStore)
		{
		}


		#region Commands
		[RelayCommand]
		private void AddStockToPortfolio(StockForView stock)
		{
			//? inject PortfolioDataStore?
		}

		public override Task GoToAddPage()
		{
			// Dodanie do PortfolioEditViewModel?
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

		#endregion
	}
}
