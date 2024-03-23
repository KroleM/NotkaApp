using ApiSharedClasses.QueryParameters;
using NotkaMobile.Helpers;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;

namespace NotkaMobile.Services
{
	public class TagDataStore : AListDataStore<TagForView, TagParameters>
	{
		public TagDataStore()
			: base()
		{
			Params = new TagParameters();
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

		public override async Task RefreshListFromService()
		{
			var PagedList = _service.TagGETAllAsync(Preferences.Default.Get("userId", 0), Params.PageNumber, Params.PageSize).Result;
			Items = PagedList.Items.ToList();
			PageParameters.CurrentPage = PagedList.CurrentPage;
			PageParameters.TotalPages = PagedList.TotalPages;
			PageParameters.PageSize = PagedList.PageSize;
			PageParameters.TotalCount = PagedList.TotalCount;
			PageParameters.HasPrevious = PagedList.HasPrevious;
			PageParameters.HasNext = PagedList.HasNext;
		}

		public override async Task<bool> UpdateItemInService(TagForView item)
		{
			return await _service.TagPUTAsync(item.Id, item).HandleRequest();
		}

		protected override void EraseParameters()
		{
			//throw new NotImplementedException();
		}
	}
}
