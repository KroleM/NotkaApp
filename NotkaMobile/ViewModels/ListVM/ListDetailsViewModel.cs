using ApiSharedClasses.QueryParameters;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;

namespace NotkaMobile.ViewModels.ListVM
{
	public class ListDetailsViewModel : AItemDetailsViewModel<ListForView, ListParameters>
	{
		public ListDetailsViewModel(ListDataStore dataStore)
			: base(dataStore)
		{
		}
		public override void LoadProperties(ListForView item)
		{
			throw new NotImplementedException();
		}
	}
}
