using NotkaMobile.Views.Notes;

namespace NotkaMobile
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(NewNotePage), typeof(NewNotePage));
		}
	}
}
