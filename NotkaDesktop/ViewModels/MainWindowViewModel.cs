using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using NotkaDesktop.Helpers;
using NotkaDesktop.Services;
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
		private RoleDataStore _roleDataStore = new RoleDataStore();
		#endregion

		#region ViewModels
		private MainPageViewModel? _mainPageViewModel;
		private UsersViewModel? _usersViewModel;
		private RolesViewModel? _rolesViewModel;
		private NewRoleViewModel? _newRoleViewModel;
		
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
		public void Destroy()
		{
			WeakReferenceMessenger.Default.UnregisterAll(this);
		}
		#endregion

		#region Private methods
		private List<CommandViewModel> CreateLeftPanelCommands()
		{
			//Here: Register for WeakReferenceMessenger
			WeakReferenceMessenger.Default.Register<ViewRequestMessage>(this, (r, m) => OpenView(m));

			return new List<CommandViewModel>
			{
				new CommandViewModel(
					"Strona główna",
					new RelayCommand(() => this.ShowMainPage())),
				new CommandViewModel(
					"Użytkownicy",
					new RelayCommand(() => this.ShowUsers())),
				new CommandViewModel(
					"Role",
					new RelayCommand(() => this.ShowRoles())),
			};
		}

		private void OpenView(ViewRequestMessage viewRequestMessage)
		{
			switch (viewRequestMessage.Value)
			{
				case MainWindowView.NewRole:
					ShowNewRole();
					break;
			}
		}

		private void ShowMainPage()
		{
			RightPanelViewModel = _mainPageViewModel;
		}
		private void ShowUsers()
		{
			if (RightPanelViewModel == _usersViewModel) return;

			_usersViewModel = new(_userDataStore);
			RightPanelViewModel = _usersViewModel;
		}
		private void ShowRoles()
		{
			if (RightPanelViewModel == _rolesViewModel) return;

			_rolesViewModel = new(_roleDataStore);
			RightPanelViewModel = _rolesViewModel;
		}
		private void ShowNewRole()
		{
			if (RightPanelViewModel == _newRoleViewModel) return;

			_newRoleViewModel = new(_roleDataStore);
			RightPanelViewModel = _newRoleViewModel;
		}
		#endregion
	}
}
