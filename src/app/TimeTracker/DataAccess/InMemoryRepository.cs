using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TimeTracker.Domain;

namespace TimeTracker.DataAccess
{
	public class InMemoryRepository : IRepository
	{
		private static IDictionary<Type, IList> tables = 
			new Dictionary<Type, IList>();

		IList<T> GetTable<T>()
		{
			var type = typeof (T);
			if (tables.ContainsKey(type))
			{
				return tables[type] as IList<T>;
			}

			var table = new List<T>();
			tables[type] = table;
			return table;
		}

		public void Insert<T>(T entity) where T : class, IEntity
		{
			GetTable<T>().Add(entity);
		}

		public IQueryable<T> Query<T>() where T : class, IEntity
		{
			return GetTable<T>().AsQueryable();
		}

		public T Get<T>(Guid id) where T : class, IEntity
		{
			return GetTable<T>().FirstOrDefault(x => x.Id == id);
		}

		public void SubmitChanges()
		{
		}

		public void Delete<T>(T entity) where T : class, IEntity
		{
			if (entity == null) return;

			GetTable<T>().Remove(entity);
		}
	}
}