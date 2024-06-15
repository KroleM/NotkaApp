using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaDesktop.ViewModels.Abstract;
using NotkaMobile.Services;
using System.Collections.ObjectModel;

namespace NotkaDesktop.ViewModels
{
	public partial class MainWindowViewModel : BaseViewModel
	{
		public EventHandler LoggedOut;

		#region DataStores
		private UserDataStore _userDataStore = new UserDataStore();
		#endregion

		#region ViewModels
		private UsersViewModel? _usersViewModel;
		private MainPageViewModel? _mainPageViewModel;
		#endregion

		#region Fields & Properties

		private ReadOnlyCollection<CommandViewModel> _leftPanelCommands;
		public ReadOnlyCollection<CommandViewModel> LeftPanelCommands
		{
			get
			{
				if (_leftPanelCommands == null)
				{
					List<CommandViewModel> cmds = this.CreateLeftPanelCommands();
					_leftPanelCommands = new ReadOnlyCollection<CommandViewModel>(cmds);
				}
				return _leftPanelCommands;
			}
		}

		[ObservableProperty]
		private BaseViewModel _rightPanelViewModel;
		#endregion

		#region Constructor
		public MainWindowViewModel() 
		{
			_mainPageViewModel = new MainPageViewModel();
			RightPanelViewModel = _mainPageViewModel;
			//_usersViewModel = new(_userDataStore);
		}
		#endregion

		#region Commands
		[RelayCommand]
		private async Task Logout()
		{
			LoggedOut?.Invoke(this, EventArgs.Empty);
		}
		#endregion

		#region Public methods

		#endregion

		#region Private methods
		private List<CommandViewModel> CreateLeftPanelCommands()
		{
			return new List<CommandViewModel>
			{
				new CommandViewModel(
					"Strona główna",
					new RelayCommand(() => this.ShowMainPage())),
				new CommandViewModel(
					"Użytkownicy",
					new RelayCommand(() => this.ShowUsers())),
				new CommandViewModel(
					"Spółki",
					new RelayCommand(() => this.ShowUsers())),
			};
		}

		private void ShowMainPage()
		{
			RightPanelViewModel = _mainPageViewModel;
		}
		private void ShowUsers()
		{
			//if (_rightPanelViewModel == _usersViewModel) return;

			//_usersViewModel.ExecuteLoadItemsCommand();
			_usersViewModel = new(_userDataStore);
			RightPanelViewModel = _usersViewModel;
			//update data in usersViewModel?
		}
		#endregion
	}
}
