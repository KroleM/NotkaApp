using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;
using NotkaMobile.ViewModels.Abstract;

namespace NotkaMobile.ViewModels
{
	public partial class SettingsViewModel : AItemDetailsViewModel<UserForView, UserParameters>
	{
		public SettingsViewModel(IDataStore<UserForView, UserParameters> dataStore) 
			: base(dataStore)
		{
			Title = "Mój profil";
		}

		[ObservableProperty]
		string _email;
		[ObservableProperty]
		string _firstName;
		[ObservableProperty]
		string _lastName;

		public override void LoadProperties(UserForView item)
		{
			Email = item.Email;
			FirstName = item.FirstName;
			LastName = item.LastName;
		}
	}
}
