using NotkaMobile.Helpers;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;

namespace NotkaMobile.Services
{
	public class NoteDataStore : AListDataStore<Note>
	{
		public NoteDataStore()
			: base()
		{
		}

		public override async Task<Note> AddItemToService(Note item)
		{
			return await _service.NotePOSTAsync(item);
		}

		public override async Task<bool> DeleteItemFromService(Note item)
		{
			return await _service.NoteDELETEAsync(Preferences.Default.Get("userId", 0), item.Id).HandleRequest();
		}

		public override async Task<Note> Find(Note item)
		{
			return await _service.NoteGETAsync(Preferences.Default.Get("userId", 0), item.Id);
		}

		public override async Task<Note> Find(int id)
		{
			return await _service.NoteGETAsync(Preferences.Default.Get("userId", 0), id);
		}

		public override async System.Threading.Tasks.Task RefreshListFromService()
		{
			items = _service.NoteAllAsync(Preferences.Default.Get("userId", 0)).Result.ToList();
		}

		public override async Task<bool> UpdateItemInService(Note item)
		{
			return await _service.NotePUTAsync(item.Id, item).HandleRequest();
		}
	}
}
