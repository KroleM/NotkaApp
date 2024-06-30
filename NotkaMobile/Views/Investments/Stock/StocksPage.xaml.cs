using NotkaMobile.ViewModels.StockVM;

namespace NotkaMobile.Views.Investments.Stock;

public partial class StocksPage : ContentPage
{
	public StocksPage(StocksViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}