using NotkaMobile.Helpers;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;

namespace NotkaMobile.Services
{
	public class NoteDataStore : AListDataStore<NoteForView, NoteParameters>
	{
		public NoteDataStore()
			: base()
		{
			Params = new NoteParameters();
		}

		public override async Task<NoteForView> AddItemToService(NoteForView item)
		{
			return await _service.NotePOSTAsync(item);
		}

		public override async Task<bool> DeleteItemFromService(NoteForView item)
		{
			return await _service.NoteDELETEAsync(Preferences.Default.Get("userId", 0), item.Id).HandleRequest();
		}

		public override async Task<NoteForView> Find(NoteForView item)
		{
			return await _service.NoteGETAsync(Preferences.Default.Get("userId", 0), item.Id);
		}

		public override async Task<NoteForView> Find(int id)
		{
			return await _service.NoteGETAsync(Preferences.Default.Get("userId", 0), id);
		}

		public override async Task RefreshListFromService()
		{
			items = _service.NoteAllAsync(Preferences.Default.Get("userId", 0), Params).Result.ToList();
		}

		public override async Task<bool> UpdateItemInService(NoteForView item)
		{
			return await _service.NotePUTAsync(item.Id, item).HandleRequest();
		}
	}
}
