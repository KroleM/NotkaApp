using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NotkaMobile.Services;
using NotkaMobile.ViewModels;
using NotkaMobile.Views;

namespace NotkaMobile
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseMauiCommunityToolkit()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
					fonts.AddFont("SansitaSwashed-Regular.ttf", "SansitaSwashedRegular");
					fonts.AddFont("SansitaSwashed-Bold.ttf", "SansitaSwashedBold");
				})
				.RegisterAppServices()
				.RegisterViewModels()
				.RegisterViews();

#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
		public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
		{
			//mauiAppBuilder.Services.AddSingleton<ViewModels.MainViewModel>();
			//DependencyService.Register<UserDataStore>();
			mauiAppBuilder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
			mauiAppBuilder.Services.AddSingleton<LoginDataStore>();
			return mauiAppBuilder;
		}
		public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
		{
			//mauiAppBuilder.Services.AddSingleton<ViewModels.MainViewModel>();
			mauiAppBuilder.Services.AddSingleton<LoginViewModel>();
			return mauiAppBuilder;
		}
		public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
		{
			mauiAppBuilder.Services.AddSingleton<LoginPage>();
			return mauiAppBuilder;
		}
	}
}
