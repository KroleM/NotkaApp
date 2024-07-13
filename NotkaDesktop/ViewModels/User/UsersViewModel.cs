using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using NotkaDesktop.Helpers;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;
using NotkaMobile.Services;

namespace NotkaDesktop.ViewModels
{
	public partial class UsersViewModel : AListViewModel<UserForView, UserParameters>
	{
		#region Constructor

		public UsersViewModel(UserDataStore dataStore, RoleDataStore roleDataStore) 
			: base("Użytkownicy", dataStore)
		{
			LoadRoles(roleDataStore);
			//IsActive = DataStore.Params.IsActive;
		}

		#endregion
		#region Fields & Properties

		private RoleDataStore _roleDataStore;

		[ObservableProperty]
		private bool _isActive = true;	//false?

		public List<RoleForView> Roles { get; private set; } = new();

		public int SelectedRoleId { get; set; } = 0;

		#endregion

		public override Task GoToAddPage()
		{
			throw new NotImplementedException();
		}

		public override async Task OnDeleteItem()
		{
			if (SelectedItem != null)
			{
				await DataStore.DeleteItemAsync(SelectedItem.Id);
			}
		}

		public override Task OnEditItem()
		{
			WeakReferenceMessenger.Default.Send(new ViewRequestMessage(MainWindowView.EditUser));
			return Task.CompletedTask;
		}

		private async Task LoadRoles(RoleDataStore roleDataStore)
		{
			_roleDataStore = roleDataStore;
			_roleDataStore.Params.PageSize = 0; // load all roles
			await _roleDataStore.RefreshListFromService();
			Roles = _roleDataStore.Items;
		}

		[RelayCommand]
		private async Task Filter()
		{
			DataStore.Params.IsActive = IsActive;
			DataStore.Params.RoleId = SelectedRoleId;
			await ExecuteLoadItemsCommand();
		}
	}
}
