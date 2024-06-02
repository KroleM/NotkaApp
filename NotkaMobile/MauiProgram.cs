using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NotkaMobile.Services;
using NotkaMobile.ViewModels;
using NotkaMobile.ViewModels.ListVM;
using NotkaMobile.ViewModels.NoteVM;
using NotkaMobile.ViewModels.TagVM;
using NotkaMobile.ViewModels.UserVM;
using NotkaMobile.Views;
using NotkaMobile.Views.Notes.List;
using NotkaMobile.Views.Notes.Note;
using NotkaMobile.Views.Notes.Tag;
using NotkaMobile.Views.User;

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
			mauiAppBuilder.Services.AddSingleton<UserDataStore>();
			mauiAppBuilder.Services.AddSingleton<NoteDataStore>();
			mauiAppBuilder.Services.AddSingleton<ListDataStore>();
			mauiAppBuilder.Services.AddSingleton<TagDataStore>();
			return mauiAppBuilder;
		}
		public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
		{
			//General
			mauiAppBuilder.Services.AddSingleton<LoginViewModel>();
			mauiAppBuilder.Services.AddTransient<SettingsViewModel>();
			mauiAppBuilder.Services.AddTransient<MainViewModel>();
			//Note
			mauiAppBuilder.Services.AddTransient<NotesViewModel>();
			mauiAppBuilder.Services.AddTransient<NoteSortFilterViewModel>();
			mauiAppBuilder.Services.AddTransient<NewNoteViewModel>();
			mauiAppBuilder.Services.AddTransient<NoteDetailsViewModel>();
			mauiAppBuilder.Services.AddTransient<NoteEditViewModel>();
			//Tag
			mauiAppBuilder.Services.AddTransient<TagsViewModel>();
			mauiAppBuilder.Services.AddTransient<NewTagViewModel>();
			mauiAppBuilder.Services.AddTransient<TagDetailsViewModel>();
			mauiAppBuilder.Services.AddTransient<TagEditViewModel>();
			//User
			mauiAppBuilder.Services.AddTransient<NewUserViewModel>();
			mauiAppBuilder.Services.AddTransient<UserDetailsViewModel>();
			mauiAppBuilder.Services.AddTransient<UserEditViewModel>();
			mauiAppBuilder.Services.AddTransient<UserPasswordEditViewModel>();
			//List
			mauiAppBuilder.Services.AddTransient<ListsViewModel>();
			mauiAppBuilder.Services.AddTransient<NewListViewModel>();
			mauiAppBuilder.Services.AddTransient<ListDetailsViewModel>();
			mauiAppBuilder.Services.AddTransient<ListEditViewModel>();
			mauiAppBuilder.Services.AddTransient<ListSortFilterViewModel>();

			return mauiAppBuilder;
		}
		public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
		{
			//General
			mauiAppBuilder.Services.AddSingleton<LoginPage>();
			mauiAppBuilder.Services.AddTransient<SettingsPage>();
			mauiAppBuilder.Services.AddTransient<MainPage>();
			//Note
			mauiAppBuilder.Services.AddTransient<NotesPage>();
			mauiAppBuilder.Services.AddTransient<NoteSortFilterPage>();
			mauiAppBuilder.Services.AddTransient<NewNotePage>();
			mauiAppBuilder.Services.AddTransient<NoteDetailsPage>();
			mauiAppBuilder.Services.AddTransient<NoteEditPage>();
			//Tag
			mauiAppBuilder.Services.AddTransient<TagsPage>();
			mauiAppBuilder.Services.AddTransient<NewTagPage>();
			mauiAppBuilder.Services.AddTransient<TagDetailsPage>();
			mauiAppBuilder.Services.AddTransient<TagEditPage>();
			//User
			mauiAppBuilder.Services.AddTransient<NewUserPage>();
			mauiAppBuilder.Services.AddTransient<UserDetailsPage>();
			mauiAppBuilder.Services.AddTransient<UserEditPage>();
			mauiAppBuilder.Services.AddTransient<UserPasswordChangePage>();
			//List
			mauiAppBuilder.Services.AddTransient<ListsPage>();
			mauiAppBuilder.Services.AddTransient<NewListPage>();
			mauiAppBuilder.Services.AddTransient<ListDetailsPage>();
			mauiAppBuilder.Services.AddTransient<ListEditPage>();
			mauiAppBuilder.Services.AddTransient<ListSortFilterPage>();

			return mauiAppBuilder;
		}
	}
}
