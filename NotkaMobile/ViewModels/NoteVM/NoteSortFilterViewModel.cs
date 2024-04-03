using ApiSharedClasses.QueryParameters;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;

namespace NotkaMobile.ViewModels.NoteVM
{
	public class NoteSortFilterViewModel : ASortFilterViewModel<NoteForView, NoteParameters>
	{
		public NoteSortFilterViewModel(NoteDataStore dataStore) 
			: base(dataStore)
		{
		}


	}
}
