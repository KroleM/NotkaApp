using ApiSharedClasses.QueryParameters;
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

		public override Task OnItemSelected(UserForView? item)
		{
			throw new NotImplementedException();
		}
	}
}
