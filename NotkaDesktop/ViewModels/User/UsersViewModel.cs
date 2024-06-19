using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.Messaging;
using NotkaDesktop.Helpers;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.ViewModels.Abstract;
using NotkaMobile.Services;

namespace NotkaDesktop.ViewModels
{
	public class UsersViewModel : AListViewModel<UserForView, UserParameters>
	{
		public UsersViewModel(UserDataStore dataStore) 
			: base("Użytkownicy", dataStore)
		{
		}

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
	}
}
