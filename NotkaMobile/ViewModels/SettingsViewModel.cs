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
		}
		[ObservableProperty]
		string _email;
		[ObservableProperty]
		string _firstName;
		[ObservableProperty]
		string _lastName;
		[ObservableProperty]
		string _name;
		public override void LoadProperties(UserForView item)
		{
			throw new NotImplementedException();
		}
	}
}
