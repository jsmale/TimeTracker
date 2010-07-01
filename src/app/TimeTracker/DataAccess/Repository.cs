using System;
using System.Linq;
using TimeTracker.Domain;

namespace TimeTracker.DataAccess
{
	public class Repository<T> : IRepository<T> where T : class, IEntity
	{
		private readonly IRepository repository;

		public Repository(IRepository repository)
		{
			this.repository = repository;
		}

		public void Insert(T entity)
		{
			repository.Insert(entity);
		}

		public IQueryable<T> Query()
		{
			return repository.Query<T>();
		}

		public T Get(Guid id)
		{
			return repository.Get<T>(id);
		}

		public void SubmitChanges()
		{
			repository.SubmitChanges();
		}

		public void Delete(T entity)
		{
			repository.Delete(entity);
		}
	}
}