using ApiSharedClasses.QueryParameters;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;
using NotkaMobile.ViewModels.Abstract;

namespace NotkaMobile.ViewModels.ListVM
{
	public class ListEditViewModel : AEditViewModel<ListForView, ListParameters>
	{
		public ListEditViewModel(string title, IDataStore<ListForView, ListParameters> dataStore) 
			: base("Edycja listy", dataStore)
		{
		}

		public override void LoadProperties()
		{
			throw new NotImplementedException();
		}

		public override ListForView SetItem()
		{
			throw new NotImplementedException();
		}

		public override bool ValidateSave()
		{
			throw new NotImplementedException();
		}
	}
}
