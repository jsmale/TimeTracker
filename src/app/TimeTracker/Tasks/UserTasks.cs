using System;
using System.Web;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using System.Linq;

namespace TimeTracker.Tasks
{
	public class UserTasks : IUserTasks
	{
		private readonly IRepository repository;

		public UserTasks(IRepository repository)
		{
			this.repository = repository;
		}

      public User GetCurrentUser()
      {
      	string name = GetCurrentUserName();
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}

      	var currentUser = repository.Query<User>().FirstOrDefault(x=>x.Username == name);
			if(currentUser ==null)
			{
				currentUser = new User {Id = Guid.NewGuid(), Username = name};
				repository.Insert(currentUser);
			}
      	return currentUser;
      }

		public virtual string GetCurrentUserName()
		{
			return HttpContext.Current.User.Identity.Name;
		}
	}
}