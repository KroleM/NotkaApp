using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using System.Linq.Expressions;

namespace NotkaAPI.Repository
{
	public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		protected NotkaDatabaseContext Context { get; set; }	//FIXME: czy set potrzebny?

		public RepositoryBase(NotkaDatabaseContext repositoryContext)
		{
			Context = repositoryContext;
		}

		public IQueryable<T> FindAll()
		{
			return Context.Set<T>()
				.AsNoTracking();
		}

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
		{
			return Context.Set<T>()
				.Where(expression)
				.AsNoTracking();
		}

		public void Create(T entity)
		{
			Context.Set<T>().Add(entity);	//AddAsync?
		}

		public void Update(T entity)
		{
			Context.Set<T>().Update(entity);
		}

		public void Delete(T entity)
		{
			Context.Set<T>().Remove(entity);
		}
	}
}
