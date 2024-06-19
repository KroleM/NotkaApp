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
		private UserEditViewModel? _userEditViewModel;
		private RolesViewModel? _rolesViewModel;
		private NewRoleViewModel? _newRoleViewModel;
		private RoleEditViewModel? _roleEditViewModel;
		
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
		private BaseViewModel _previousRightPanelViewModel;
		private MainWindowView _previousRightPanelType;
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
				case MainWindowView.Cancel:
					CancelView();
					break;
				case MainWindowView.BackAndRefresh:
					GoBackAndRefresh();
					break;
				case MainWindowView.Users:
					ShowUsers();
					break;
				case MainWindowView.EditUser:
					ShowEditUser();
					break;
				case MainWindowView.Roles:	//??
					ShowRoles();
					break;
				case MainWindowView.NewRole:
					ShowNewRole();
					break;
				case MainWindowView.EditRole:
					ShowEditRole();
					break;
			}
		}
		private void CancelView()
		{
			if (_previousRightPanelViewModel == null)
			{
				RightPanelViewModel = _mainPageViewModel;
			}
			else
			{
				RightPanelViewModel = _previousRightPanelViewModel;
			}
		}
		private void GoBackAndRefresh()
		{
			if (_previousRightPanelViewModel == null)
			{
				RightPanelViewModel = _mainPageViewModel;
			}
			else
			{
				WeakReferenceMessenger.Default.Send(new ViewRequestMessage(_previousRightPanelType));
			}
		}
		private void ShowMainPage()
		{
			RightPanelViewModel = _mainPageViewModel;
		}
		private void ShowUsers()
		{
			if (RightPanelViewModel == _usersViewModel) return;	//could cause bad behavior when using Delete

			_usersViewModel = new(_userDataStore);
			RightPanelViewModel = _usersViewModel;
		}
		private void ShowEditUser()
		{
			if (RightPanelViewModel == _userEditViewModel) return;	//cannot be invoked if UserEdit already invoked
			if (RightPanelViewModel is not UsersViewModel) return;	//can only be invoked from Users View (Model)

			_previousRightPanelType = MainWindowView.Users;
			_previousRightPanelViewModel = RightPanelViewModel;
			int itemId = (RightPanelViewModel as UsersViewModel).SelectedItem.Id; //int for loading particular item
			(RightPanelViewModel as UsersViewModel).SelectedItem = null;	//cancel the selection
			_userEditViewModel = new(_userDataStore, _roleDataStore, itemId);
			RightPanelViewModel = _userEditViewModel;	//change view
		}
		private void ShowRoles()
		{
			_rolesViewModel = new(_roleDataStore);
			RightPanelViewModel = _rolesViewModel;
		}
		private void ShowNewRole()
		{
			if (RightPanelViewModel == _newRoleViewModel) return;

			_previousRightPanelType = MainWindowView.Roles;
			_previousRightPanelViewModel = RightPanelViewModel;			
			_newRoleViewModel = new(_roleDataStore);
			RightPanelViewModel = _newRoleViewModel;
		}
		private void ShowEditRole()
		{
			if (RightPanelViewModel == _roleEditViewModel) return;
			if (RightPanelViewModel is not RolesViewModel) return;

			_previousRightPanelType = MainWindowView.Roles;
			_previousRightPanelViewModel = RightPanelViewModel;
			int itemId = (RightPanelViewModel as RolesViewModel).SelectedItem.Id;
			(RightPanelViewModel as RolesViewModel).SelectedItem = null;
			_roleEditViewModel = new(_roleDataStore, itemId);
			RightPanelViewModel = _roleEditViewModel;
		}
		#endregion
	}
}
