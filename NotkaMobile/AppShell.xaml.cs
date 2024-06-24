using NotkaMobile.Views;
using NotkaMobile.Views.Feed;
using NotkaMobile.Views.Notes.List;
using NotkaMobile.Views.Notes.Note;
using NotkaMobile.Views.Notes.Tag;
using NotkaMobile.Views.User;

namespace NotkaMobile
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			//Global Routes:
			//Notes
			Routing.RegisterRoute(nameof(NewNotePage), typeof(NewNotePage));
			Routing.RegisterRoute(nameof(NoteDetailsPage), typeof(NoteDetailsPage));
			Routing.RegisterRoute(nameof(NoteEditPage), typeof(NoteEditPage));
			Routing.RegisterRoute(nameof(NoteSortFilterPage), typeof(NoteSortFilterPage));
			//Lists
			Routing.RegisterRoute(nameof(NewListPage), typeof(NewListPage));
			Routing.RegisterRoute(nameof(ListDetailsPage), typeof(ListDetailsPage));
			Routing.RegisterRoute(nameof(ListEditPage), typeof(ListEditPage));
			Routing.RegisterRoute(nameof(ListSortFilterPage), typeof(ListSortFilterPage));
			//Tags
			Routing.RegisterRoute(nameof(NewTagPage), typeof(NewTagPage));
			Routing.RegisterRoute(nameof(TagDetailsPage), typeof(TagDetailsPage));
			Routing.RegisterRoute(nameof(TagEditPage), typeof(TagEditPage));
			//Users
			Routing.RegisterRoute(nameof(NewUserPage), typeof(NewUserPage));
			Routing.RegisterRoute(nameof(UserDetailsPage), typeof(UserDetailsPage));
			Routing.RegisterRoute(nameof(UserEditPage), typeof(UserEditPage));
			Routing.RegisterRoute(nameof(UserPasswordChangePage), typeof(UserPasswordChangePage));
			//Settings
			Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
			//Feed
			Routing.RegisterRoute(nameof(FeedsPage), typeof(FeedsPage));
			Routing.RegisterRoute(nameof(FeedDetailsPage), typeof(FeedDetailsPage));
		}
	}
}
