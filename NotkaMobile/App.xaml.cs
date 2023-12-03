using NotkaMobile.Service.Reference;
using NotkaMobile.Services;

namespace NotkaMobile
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
		}

		// https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/app-lifecycle?view=net-maui-8.0
		protected override Window CreateWindow(IActivationState? activationState)
		{
			Window window = base.CreateWindow(activationState);

			window.Created += OnDestroying;

			return window;
		}
		private void OnDestroying(object? sender, EventArgs e)
		{
			Preferences.Default.Remove("userId");
		}
	}
}
