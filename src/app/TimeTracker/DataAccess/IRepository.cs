using System;
using System.Linq;
using TimeTracker.Domain;

namespace TimeTracker.DataAccess
{
	public interface IRepository<T> where T : class, IEntity
	{
		void Insert(T entity);
		IQueryable<T> Query();
		T Get(Guid id);
		void SubmitChanges();
		void Delete(T entity);
	}

	public interface IRepository
	{
		void Insert<T>(T entity) where T : class, IEntity;
		IQueryable<T> Query<T>() where T : class, IEntity;
		T Get<T>(Guid id) where T : class, IEntity;
		void SubmitChanges();
		void Delete<T>(T entity) where T : class, IEntity;
	}
}