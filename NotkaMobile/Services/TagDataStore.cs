using NotkaMobile.Helpers;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;

namespace NotkaMobile.Services
{
	public class TagDataStore : AListDataStore<TagForView>
	{
		public TagDataStore()
			: base()
		{
		}

		public override async Task<TagForView> AddItemToService(TagForView item)
		{
			return await _service.TagPOSTAsync(item);
		}

		public override async Task<bool> DeleteItemFromService(TagForView item)
		{
			return await _service.TagDELETEAsync(Preferences.Default.Get("userId", 0), item.Id).HandleRequest();
		}

		public override async Task<TagForView> Find(TagForView item)
		{
			return await _service.TagGETAsync(Preferences.Default.Get("userId", 0), item.Id);
		}

		public override async Task<TagForView> Find(int id)
		{
			return await _service.TagGETAsync(Preferences.Default.Get("userId", 0), id);
		}

		public override async System.Threading.Tasks.Task RefreshListFromService()
		{
			items = _service.TagAllAsync(Preferences.Default.Get("userId", 0)).Result.ToList();
		}

		public override async Task<bool> UpdateItemInService(TagForView item)
		{
			return await _service.TagPUTAsync(item.Id, item).HandleRequest();
		}
	}
}
