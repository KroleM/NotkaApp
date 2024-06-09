using NotkaDesktop.ViewModels;
using NotkaDesktop.Views;
using System.Windows;

namespace NotkaDesktop
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			MainWindow window = new MainWindow();
			window.DataContext = new ApplicationViewModel();
			window.Show();
		}
	}

}
