using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
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
		RoleForView _newRole;

		[ObservableProperty]
		string _firstName = string.Empty;

		[ObservableProperty]
		string _lastName = string.Empty;

		[ObservableProperty]
		DateTime? _birthDate;

		[ObservableProperty]
		bool _isActive = true;

		[ObservableProperty]
		string _newPassword = string.Empty;

		[ObservableProperty]
		string _repeatPassword = string.Empty;

		#endregion
		#region Methods
		public override void LoadProperties()
		{
			FirstName = Item.FirstName;
			LastName = Item.LastName;
			IsActive = Item.IsActive;
			BirthDate = Item.BirthDate?.DateTime;

			SelectedRoles.Clear();
			foreach (var role in Item.RolesForView)
			{
				SelectedRoles.Add(role);
			}
		}

		public override UserForView SetItem()
		{
			Item.FirstName = this.FirstName;
			Item.LastName = this.LastName;
			Item.IsActive = this.IsActive;
			Item.BirthDate = this.BirthDate;
			Item.ModifiedDate = DateTimeOffset.Now;
			Item.RolesForView = this.SelectedRoles;
			Item.Password = this.NewPassword;
			return Item;
		}

		public override bool ValidateSave()
		{
			return SelectedRoles.Any()
				&& !string.IsNullOrWhiteSpace(NewPassword) ? true : NewPassword == RepeatPassword;
		}

		private async Task LoadRoles(RoleDataStore roleDataStore)
		{
			_roleDataStore = roleDataStore;
			_roleDataStore.Params.PageSize = 0;
			await _roleDataStore.RefreshListFromService();
			Roles = _roleDataStore.Items;
		}
		#endregion
		#region Commands
		[RelayCommand]
		private void AddRole()
		{
			if (NewRole != null && !SelectedRoles.Select(sr => sr.Id).Contains(NewRole.Id))
			{
				SelectedRoles.Add(NewRole);
			}
		}
		[RelayCommand]
		private void RemoveRole(RoleForView role)
		{
			if (SelectedRoles.Contains(role))
			{
				SelectedRoles.Remove(role);
			}
		}
		#endregion
	}
}
