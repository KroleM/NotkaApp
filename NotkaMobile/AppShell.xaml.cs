using NotkaMobile.Views;
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
			//Tags
			Routing.RegisterRoute(nameof(NewTagPage), typeof(NewTagPage));
			Routing.RegisterRoute(nameof(TagDetailsPage), typeof(TagDetailsPage));
			Routing.RegisterRoute(nameof(TagEditPage), typeof(TagEditPage));
			//Users
			Routing.RegisterRoute(nameof(NewUserPage), typeof(NewUserPage));
			//Settings
			Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
		}
	}
}
