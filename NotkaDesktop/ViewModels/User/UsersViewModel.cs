using ApiSharedClasses.QueryParameters;
using ApiSharedClasses.SortValues;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using NotkaDesktop.Helpers;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;
using NotkaMobile.Services;
using System.Collections.ObjectModel;

namespace NotkaDesktop.ViewModels
{
	public partial class UsersViewModel : AListViewModel<UserForView, UserParameters>
	{
		#region Constructor

		public UsersViewModel(UserDataStore dataStore, RoleDataStore roleDataStore) 
			: base("Użytkownicy", dataStore)
		{
			LoadRoles(roleDataStore);
			IsActive = DataStore.Params.IsActive;
			SelectedRole = Roles.FirstOrDefault(x => x.Id == DataStore.Params.RoleId);
			SearchPhrase = DataStore.Params.SearchPhrase;
			CreateSortItems();
		}

		#endregion
		#region Fields & Properties

		private RoleDataStore _roleDataStore;

		[ObservableProperty]
		private bool _isActive = true;	//false?

		public List<RoleForView> Roles { get; private set; } = new();

		[ObservableProperty]
		private RoleForView? _selectedRole;

		public ObservableCollection<SortClass> SortItems { get; } = new();

		[ObservableProperty]
		private SortClass? _selectedSortValue;

		[ObservableProperty]
		private string? _searchPhrase;

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
			Roles.Insert(0, new RoleForView
			{
				Id = 0,
				Name = "Dowolna",
				IsActive = true,
			});
			SelectedRole = Roles[0];
		}

		private void CreateSortItems()
		{
			SortItems.Clear();
			SortItems.Add(new SortClass(UserSortValue.EmailFromAtoZ, "E-mail od A do Z"));
			SortItems.Add(new SortClass(UserSortValue.EmailFromZtoA, "E-mail od Z do A"));
			SortItems.Add(new SortClass(UserSortValue.FirstNameFromAtoZ, "Imię od A do Z"));
			SortItems.Add(new SortClass(UserSortValue.FirstNameFromZtoA, "Imię od Z do A"));
			SortItems.Add(new SortClass(UserSortValue.LastNameFromAtoZ, "Nazwisko od A do Z"));
			SortItems.Add(new SortClass(UserSortValue.LastNameFromZtoA, "Nazwisko od Z do A"));
		}

		[RelayCommand]
		private async Task Filter()
		{
			DataStore.Params.SortOrder = SelectedSortValue?.SortEnum.ToString() ?? string.Empty;
			DataStore.Params.IsActive = IsActive;
			DataStore.Params.RoleId = SelectedRole?.Id ?? 0;
			await ExecuteLoadItemsCommand();
			LoadMoreItemsCommand.NotifyCanExecuteChanged();
		}

		[RelayCommand]
		private async Task ClearChoices()
		{
			CreateSortItems();
			SelectedSortValue = null;
			SelectedRole = Roles[0];
			IsActive = false;
			DataStore.Params.SearchPhrase = SearchPhrase = null;

			await Filter();
		}

		[RelayCommand]
		private async Task Search()
		{
			DataStore.Params.SearchPhrase = SearchPhrase;
			await ExecuteLoadItemsCommand();
			LoadMoreItemsCommand.NotifyCanExecuteChanged();
		}
	}
}
