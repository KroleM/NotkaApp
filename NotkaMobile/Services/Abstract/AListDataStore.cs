using NotkaMobile.Service.Reference;

namespace NotkaMobile.Services.Abstract
{
	public abstract class AListDataStore<T, U> : ADataStore, IDataStore<T, U> 
		where T : class
		where U : class
	{
		public List<T> items = new List<T>();
		public U Params {  get; set; }

		public AListDataStore()
			: base()
		{
			//I comment it, because it causes double item loading
			//RefreshListFromService();
		}
		public async Task<bool> AddItemAsync(T item)
		{
			items.Add(await AddItemToService(item));

			return await Task.FromResult(true);
		}
		public abstract Task<T> Find(T item);
		public abstract Task<T> Find(int id);
		public abstract Task RefreshListFromService(U parameters);
		public abstract Task<bool> DeleteItemFromService(T item);
		public abstract Task<bool> UpdateItemInService(T item);
		public abstract Task<T> AddItemToService(T item);


		public async Task<bool> UpdateItemAsync(T item)
		{
			await UpdateItemInService(item);
			await RefreshListFromService();//RefreshListFromService();
			return await Task.FromResult(true);
		}

		public async Task<bool> DeleteItemAsync(int id)
		{
			var oldItem = await Find(id);
			items.Remove(oldItem);
			await DeleteItemFromService(oldItem);
			await RefreshListFromService();
			return await Task.FromResult(true);
		}

		public async Task<T> GetItemAsync(int id)
		{
			return await Task.FromResult(await Find(id));
		}

		public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
		{
			await RefreshListFromService();
			return await Task.FromResult(items);
		}
	}
}
