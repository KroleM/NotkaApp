using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Models.Notes;

namespace NotkaAPI.Repository
{
	public class TagRepository : RepositoryBase<Tag>//, INoteRepository
	{
		public TagRepository(NotkaDatabaseContext repositoryContext)
			: base(repositoryContext)
		{
		}
	}
}
