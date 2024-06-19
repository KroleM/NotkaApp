using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.Messaging;
using NotkaDesktop.Helpers;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;

namespace NotkaDesktop.ViewModels
{
	public class RolesViewModel : AListViewModel<RoleForView, RoleParameters>
	{
		public RolesViewModel(RoleDataStore dataStore) 
			: base("Role", dataStore)
		{
		}

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
