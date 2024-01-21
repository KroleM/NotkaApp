using NotkaMobile.Views.Notes.Note;
using NotkaMobile.Views.Notes.Tag;

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
			//Tags
			Routing.RegisterRoute(nameof(NewTagPage), typeof(NewTagPage));
			Routing.RegisterRoute(nameof(TagDetailsPage), typeof(TagDetailsPage));
		}
	}
}
