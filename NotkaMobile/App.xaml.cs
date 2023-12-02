namespace NotkaMobile
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}
	}
}
