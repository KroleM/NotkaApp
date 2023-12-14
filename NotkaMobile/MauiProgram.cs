using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NotkaMobile.Services;
using NotkaMobile.ViewModels;
using NotkaMobile.ViewModels.NoteVM;
using NotkaMobile.ViewModels.TagVM;
using NotkaMobile.Views;
using NotkaMobile.Views.Notes;

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
			mauiAppBuilder.Services.AddSingleton<NoteDataStore>();
			mauiAppBuilder.Services.AddSingleton<TagDataStore>();
			return mauiAppBuilder;
		}
		public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
		{
			//mauiAppBuilder.Services.AddSingleton<ViewModels.MainViewModel>();
			mauiAppBuilder.Services.AddSingleton<LoginViewModel>();
			mauiAppBuilder.Services.AddTransient<NotesViewModel>();
			mauiAppBuilder.Services.AddTransient<NewNoteViewModel>();
			mauiAppBuilder.Services.AddTransient<NoteDetailsViewModel>();
			mauiAppBuilder.Services.AddTransient<TagsViewModel>();
			mauiAppBuilder.Services.AddTransient<NewTagViewModel>();

			return mauiAppBuilder;
		}
		public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
		{
			mauiAppBuilder.Services.AddSingleton<LoginPage>();
			mauiAppBuilder.Services.AddTransient<NotesPage>();
			mauiAppBuilder.Services.AddTransient<NewNotePage>();
			mauiAppBuilder.Services.AddTransient<TagsPage>();
			mauiAppBuilder.Services.AddTransient<NewTagPage>();

			return mauiAppBuilder;
		}
	}
}
