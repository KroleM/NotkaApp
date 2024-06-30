using ApiSharedClasses.QueryParameters;
using NotkaMobile.Helpers;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;

namespace NotkaMobile.Services
{
	public class PortfolioDataStore : AListDataStore<PortfolioForView, PortfolioParameters>
	{
		public PortfolioDataStore()
			: base()
		{
			Params = new PortfolioParameters();
		}

		public override async Task<PortfolioForView> AddItemToService(PortfolioForView item)
		{
			return await _service.PortfolioPOSTAsync(item);
		}

		public override async Task<bool> DeleteItemFromService(PortfolioForView item)
		{
			return await _service.PortfolioDELETEAsync(Preferences.Default.Get("userId", 0), item.Id).HandleRequest();
		}

		public override async Task<PortfolioForView> Find(PortfolioForView item)
		{
			return await _service.PortfolioGETAsync(Preferences.Default.Get("userId", 0), item.Id);
		}

		public override async Task<PortfolioForView> Find(int id)
		{
			return await _service.PortfolioGETByUserAsync(Preferences.Default.Get("userId", 0));
		}

		public override async Task RefreshListFromService()
		{
			//throw new NotImplementedException();
		}

		public override async Task<bool> UpdateItemInService(PortfolioForView item)
		{
			return await _service.PortfolioPUTAsync(item.Id, item).HandleRequest();
		}

		protected override void EraseParameters()
		{
			//throw new NotImplementedException();
		}
	}
}
