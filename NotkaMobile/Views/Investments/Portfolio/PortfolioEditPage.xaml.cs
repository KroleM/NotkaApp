using NotkaMobile.ViewModels.PortfolioVM;

namespace NotkaMobile.Views.Investments.Portfolio;

public partial class PortfolioEditPage : ContentPage
{
	public PortfolioEditPage(PortfolioEditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}