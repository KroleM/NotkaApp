using ApiSharedClasses.QueryParameters;
using ApiSharedClasses.SortValues;
using NotkaMobile.Helpers;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;

namespace NotkaMobile.ViewModels.NoteVM
{
	public partial class NoteSortFilterViewModel : ASortFilterViewModel<NoteForView, NoteParameters>
	{
		public NoteSortFilterViewModel(NoteDataStore dataStore) 
			: base(dataStore)
		{
		}

		protected override void CreateSortItems()
		{
			SortItems.Clear();
			SortItems.Add(new SortClass(NoteSortValue.FromAtoZ, "Od A do Z"));
			SortItems.Add(new SortClass(NoteSortValue.FromZtoA, "Od Z do A"));
			SortItems.Add(new SortClass(NoteSortValue.ByCreationDateAscending, "Według daty utworzenia rosnąco"));
			SortItems.Add(new SortClass(NoteSortValue.ByCreationDateDescending, "Według daty utworzenia malejąco"));
			SortItems.Add(new SortClass(NoteSortValue.ByModificationDateAscending, "Według daty modyfikacji rosnąco"));
			SortItems.Add(new SortClass(NoteSortValue.ByModificationDateDescending, "Według daty modyfikacji malejąco"));
		}

		public override async Task OnExecute()
		{
			DataStore.Params.SortOrder = SelectedSortValue?.SortEnum.ToString() ?? string.Empty;
			await base.OnExecute();
		}
		public override async Task OnClear()
		{			
			CreateSortItems();
			SelectedSortValue = null;			
		}
	}
}
