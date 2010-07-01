using System;
using System.Data.Linq;
using System.Linq;
using TimeTracker.Domain;

namespace TimeTracker.DataAccess
{
	public class LinqToSqlRepository : IRepository
	{
		private readonly DataContext dataContext;

		public LinqToSqlRepository(DataContext dataContext)
		{
			this.dataContext = dataContext;
		}

		public void Insert<T>(T entity) where T : class, IEntity
		{
			Table<T>().InsertOnSubmit(entity);
		}

		private Table<T> Table<T>() where T : class, IEntity
		{
			return dataContext.GetTable<T>();
		}

		public IQueryable<T> Query<T>() where T : class, IEntity
		{
			return Table<T>();
		}

		public T Get<T>(Guid id) where T : class, IEntity
		{
			return Table<T>().FirstOrDefault(x => Equals(x.Id, id));
		}

		public void SubmitChanges()
		{
			dataContext.SubmitChanges();
		}

		public void Delete<T>(T entity) where T : class, IEntity
		{
			if (entity == null) return;
			Table<T>().DeleteOnSubmit(entity);
		}
	}
}