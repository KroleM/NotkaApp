using NotkaMobile.ViewModels.FeedVM;

namespace NotkaMobile.Views.Feed;

public partial class FeedsPage : ContentPage
{
	public FeedsPage(FeedsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}