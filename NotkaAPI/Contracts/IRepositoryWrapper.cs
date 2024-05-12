namespace NotkaAPI.Contracts
{
	public interface IRepositoryWrapper
	{
		IUserRepository User { get; }
		INoteRepository Note { get; }
		ITagRepository Tag { get; }
		Task SaveAsync();
	}
}
