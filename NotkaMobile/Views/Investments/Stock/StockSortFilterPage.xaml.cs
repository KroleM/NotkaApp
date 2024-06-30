using NotkaMobile.ViewModels.StockVM;

namespace NotkaMobile.Views.Investments.Stock;

public partial class StockSortFilterPage : ContentPage
{
	public StockSortFilterPage(StockSortFilterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}