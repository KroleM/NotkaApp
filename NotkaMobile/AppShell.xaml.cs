using NotkaMobile.Views.Notes;

namespace NotkaMobile
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			//Notes
			Routing.RegisterRoute(nameof(NewNotePage), typeof(NewNotePage));
			Routing.RegisterRoute(nameof(NoteDetailsPage), typeof(NoteDetailsPage));
			//Tags
			Routing.RegisterRoute(nameof(NewTagPage), typeof(NewTagPage));
			Routing.RegisterRoute(nameof(TagDetailsPage), typeof(TagDetailsPage));
		}
	}
}
