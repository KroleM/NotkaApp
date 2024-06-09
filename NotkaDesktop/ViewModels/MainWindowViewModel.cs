using NotkaDesktop.ViewModels.Abstract;
using NotkaMobile.Services;

namespace NotkaDesktop.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		public EventHandler LoggedOut;

		#region DataStores
		private UserDataStore _userDataStore = new UserDataStore();
		#endregion

		#region ViewModels
		
		#endregion

		#region Fields & Properties

		#endregion

		#region Constructor
		public MainWindowViewModel() 
		{

		}
		#endregion
	}
}
