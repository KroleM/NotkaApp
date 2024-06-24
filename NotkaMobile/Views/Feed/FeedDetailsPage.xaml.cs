using NotkaMobile.ViewModels.FeedVM;

namespace NotkaMobile.Views.Feed;

public partial class FeedDetailsPage : ContentPage
{
	public FeedDetailsPage(FeedDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}