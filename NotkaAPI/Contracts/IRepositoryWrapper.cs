namespace NotkaAPI.Contracts
{
	public interface IRepositoryWrapper
	{
		INoteRepository Note { get; }
		ITagRepository Tag { get; }
		Task SaveAsync();
	}
}
