using NotkaMobile.ViewModels.StockVM;

namespace NotkaMobile.Views.Investments.Stock;

public partial class StockDetailsPage : ContentPage
{
	public StockDetailsPage(StockDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}