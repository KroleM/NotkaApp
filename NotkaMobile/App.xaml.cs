using NotkaMobile.Service.Reference;
using NotkaMobile.Services;

namespace NotkaMobile
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			DependencyService.Register<TagDataStore>();
			DependencyService.Register<NoteDataStore>();

			MainPage = new AppShell();
		}

		// https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/app-lifecycle?view=net-maui-8.0
		protected override Window CreateWindow(IActivationState? activationState)
		{
			Window window = base.CreateWindow(activationState);

			window.Deactivated += OnDestroying;
			//window.Destroying += OnDestroying;	// FIXME doesn't work on Android

			return window;
		}
		private void OnDestroying(object? sender, EventArgs e)
		{
			Preferences.Default.Remove("userId");
			Preferences.Default.Remove("passwordHash");
		}
	}
}
