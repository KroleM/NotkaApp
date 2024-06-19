using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.Services.Abstract;
using NotkaDesktop.ViewModels.Abstract;
using NotkaMobile.Services;
using System.Collections.ObjectModel;

namespace NotkaDesktop.ViewModels
{
	public partial class UserEditViewModel : AEditViewModel<UserForView, UserParameters>
	{
		public UserEditViewModel(UserDataStore dataStore, RoleDataStore roleDataStore, int itemId) 
			: base("Edycja użytkownika", dataStore, itemId)
		{
			//RolesDataStore, aby pobrać możliwe role!
			LoadRoles(roleDataStore);
		}
		#region Fields & Properties
		private RoleDataStore _roleDataStore;
		public List<RoleForView> Roles { get; set; } = new();

		[ObservableProperty]
		ObservableCollection<RoleForView> _selectedRoles = new();

		[ObservableProperty]
		string _firstName = string.Empty;

		[ObservableProperty]
		string _lastName = string.Empty;

		[ObservableProperty]
		DateTime? _birthDate;

		[ObservableProperty]
		bool _isActive = true;
		#endregion
		#region Methods
		public override void LoadProperties()
		{
			FirstName = Item.FirstName;
			LastName = Item.LastName;
			IsActive = Item.IsActive;
			BirthDate = Item.BirthDate?.DateTime;
		}

		public override UserForView SetItem()
		{
			Item.FirstName = this.FirstName;
			Item.LastName = this.LastName;
			Item.IsActive = this.IsActive;
			Item.BirthDate = this.BirthDate;
			Item.ModifiedDate = DateTimeOffset.Now;
			return Item;
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(FirstName);
			//rola też musi być przydzielona!
		}

		private async Task LoadRoles(RoleDataStore roleDataStore)
		{
			_roleDataStore = roleDataStore;
			_roleDataStore.Params.PageSize = 0;
			await _roleDataStore.RefreshListFromService();
			Roles = _roleDataStore.Items;
		}
		#endregion
	}
}
