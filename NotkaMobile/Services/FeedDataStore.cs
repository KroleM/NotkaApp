using ApiSharedClasses.QueryParameters;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;

namespace NotkaMobile.Services
{
	public class FeedDataStore : AListDataStore<FeedForView, FeedParameters>
	{
		public FeedDataStore()
			: base()
		{
			Params = new FeedParameters();
			Params.IsActive = true;
		}
		public override Task<FeedForView> AddItemToService(FeedForView item)
		{
			throw new NotImplementedException();
		}

		public override Task<bool> DeleteItemFromService(FeedForView item)
		{
			throw new NotImplementedException();
		}

		public override async Task<FeedForView> Find(FeedForView item)
		{
			return await _service.FeedGETAsync(Preferences.Default.Get("userId", 0), item.Id);
		}

		public override async Task<FeedForView> Find(int id)
		{
			return await _service.FeedGETAsync(Preferences.Default.Get("userId", 0), id);
		}

		public override async Task RefreshListFromService()
		{
			var PagedList = _service.FeedGETAllAsync(Preferences.Default.Get("userId", 0), Params.IsActive, Params.PageNumber, Params.PageSize, Params.SortOrder, Params.SearchPhrase).Result;
			Items = PagedList.Items.ToList();
			PageParameters.CurrentPage = PagedList.CurrentPage;
			PageParameters.TotalPages = PagedList.TotalPages;
			PageParameters.PageSize = PagedList.PageSize;
			PageParameters.TotalCount = PagedList.TotalCount;
			PageParameters.HasPrevious = PagedList.HasPrevious;
			PageParameters.HasNext = PagedList.HasNext;
		}

		public override Task<bool> UpdateItemInService(FeedForView item)
		{
			throw new NotImplementedException();
		}

		protected override void EraseParameters()
		{
			//throw new NotImplementedException();
		}
	}
}
