﻿using NotkaMobile.Service.Reference;
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

			window.Activated += OnActivated;
#if ANDROID
			//window.Deactivated += OnDestroying;	// FIXME works on Android, but causes strange behaviour (e.g. logout when media picker window is opened)
#endif
			//window.Destroying += OnDestroying;	// FIXME doesn't work on Android

			return window;
		}
		private async void OnActivated(object? sender, EventArgs e)
		{
#if WINDOWS
				const int DefaultWidth = 520;
				const int DefaultHeight = 800;

				var window = sender as Window;

				// change window size.
				window.Width = DefaultWidth;
				window.Height = DefaultHeight;

				// give it some time to complete window resizing task.
				await window.Dispatcher.DispatchAsync(() => { });

				var disp = DeviceDisplay.Current.MainDisplayInfo;

				// move to screen center
				window.X = (disp.Width / disp.Density - window.Width) / 2;
				window.Y = (disp.Height / disp.Density - window.Height) / 2;
#endif
		}
		private void OnDestroying(object? sender, EventArgs e)
		{
			Preferences.Default.Remove("userId");
			Preferences.Default.Remove("passwordHash");
		}
	}
}
