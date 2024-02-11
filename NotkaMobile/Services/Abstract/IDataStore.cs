namespace NotkaMobile.Services.Abstract
{
	public interface IDataStore<T, U>
	{
		U Params { get; set; }
		Task<bool> AddItemAsync(T item);
		Task<bool> UpdateItemAsync(T item);
		Task<bool> DeleteItemAsync(int id);
		Task<T> GetItemAsync(int id);
		Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
	}
}
