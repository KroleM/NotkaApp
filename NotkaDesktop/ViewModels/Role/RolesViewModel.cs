using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using NotkaDesktop.Helpers;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;
using NotkaMobile.Services;

namespace NotkaDesktop.ViewModels
{
	public partial class RolesViewModel : AListViewModel<RoleForView, RoleParameters>
	{
		public RolesViewModel(RoleDataStore dataStore, UserDataStore userDataStore)
			: base("Role", dataStore)
		{
			_userDataStore = userDataStore;
			_userDataStore.Params.RoleId = 0;
		}

		#region Fields & Properties

		private UserDataStore _userDataStore;

		private RoleForView? _selectedRole;
		public RoleForView? SelectedRole
		{
			get => _selectedRole;
			set
			{
				if (_selectedRole != value)
				{
					SelectedItem = value;
					_selectedRole = value;
					OnPropertyChanged(nameof(SelectedRole));

					if (UsersViewModel == null)
					{
						UsersViewModel = new UsersViewModel(_userDataStore, (RoleDataStore)DataStore);
					}
					_userDataStore.Params.RoleId = _selectedRole?.Id ?? 0;
					UsersViewModel.ExecuteLoadItemsCommand();
					UsersViewModel.LoadMoreItemsCommand.NotifyCanExecuteChanged();
					//OnPropertyChanged(nameof(UsersViewModel));
				}
			}
		}

		[ObservableProperty]
		private UsersViewModel? _usersViewModel;

		#endregion

		public override Task GoToAddPage()
		{
			WeakReferenceMessenger.Default.Send(new ViewRequestMessage(MainWindowView.NewRole));
			return Task.CompletedTask;
		}

		public override async Task OnDeleteItem()
		{
			if (SelectedItem != null)
			{
				await DataStore.DeleteItemAsync(SelectedItem.Id);
			}
			WeakReferenceMessenger.Default.Send(new ViewRequestMessage(MainWindowView.Roles));
		}

		public override Task OnEditItem()
		{
			WeakReferenceMessenger.Default.Send(new ViewRequestMessage(MainWindowView.EditRole));
			return Task.CompletedTask;
		}
	}
}
