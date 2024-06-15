using CommunityToolkit.Mvvm.ComponentModel;
using NotkaDesktop.ViewModels.Abstract;
using NotkaMobile.Services;

namespace NotkaDesktop.ViewModels
{
	public partial class ApplicationViewModel : ObservableObject
	{
		#region DataStores
		private UserDataStore _userDataStore = new UserDataStore();
		#endregion

		#region ViewModels
		[ObservableProperty]
		private BaseViewModel _currentViewModel;
		private LoginViewModel _loginViewModel;
		private MainWindowViewModel? _mainWindowViewModel;
		#endregion

		#region Fields & Properties
		public static int s_userId;
		#endregion

		#region Constructor
		public ApplicationViewModel()
		{
			_loginViewModel = new LoginViewModel(_userDataStore);
			_loginViewModel.LoggedIn += OnLoggedIn;
			//_mainWindowViewModel = new MainWindowViewModel();
			//_mainWindowViewModel.LoggedOut += OnLoggedOut;
			_currentViewModel = _loginViewModel;
			
		}
		#endregion


		private void OnLoggedIn(object source, EventArgs eventArgs)
		{
			_mainWindowViewModel = new MainWindowViewModel();
			_mainWindowViewModel.LoggedOut += OnLoggedOut;
			CurrentViewModel = _mainWindowViewModel;
		}
		private void OnLoggedOut(object source, EventArgs eventArgs)
		{
			CurrentViewModel = _loginViewModel;
			s_userId = 0;
			_mainWindowViewModel.LoggedOut -= OnLoggedOut;
		}
	}
}
