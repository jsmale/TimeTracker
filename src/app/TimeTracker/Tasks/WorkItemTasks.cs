using System;
using System.Collections.Generic;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;
using System.Linq;

namespace TimeTracker.Tasks
{
	public class WorkItemTasks : IWorkItemTasks
	{
		private readonly IRepository repository;
		private readonly IUserTasks userTasks;

		public WorkItemTasks(IRepository repository, IUserTasks userTasks  )
		{
			this.repository = repository;
			this.userTasks = userTasks;
		}

		public Guid Create(CreateWorkItemRequest createWorkItemRequest)
		{
			var projTaskType=repository.Get<ProjectTaskType>(
				createWorkItemRequest.ProjectTaskTypeId);
			var id = Guid.NewGuid();	
			var user = userTasks.GetCurrentUser();

			repository.Insert(new WorkItem()
			                  {
										Id = id,
			                  	Name = createWorkItemRequest.Name,
			                  	ProjectTaskType = projTaskType,
			                  	User = user
			                  });

			return id;
		}

		public IQueryable<WorkItemListItem> GetWorkItems()
		{
			return GetWorkItems(null);
		}
		public IQueryable<WorkItemListItem> GetWorkItems(string search)
		{
			var currentUser = userTasks.GetCurrentUser();

			var query = (from wi in repository.Query<WorkItem>()
					  let timeEntry = wi.TimeEntries.OrderByDescending(x => x.StartTime).FirstOrDefault()
						where wi.User == currentUser
						select new WorkItemListItem
								 {
	                   		Id = wi.Id,
	                   		Name = wi.Name,
	                   		ProjectTaskTypeName = wi.ProjectTaskType.Name,
									LastStartTime = (timeEntry != null) ? timeEntry.StartTime : DateTime.MaxValue
								 });

			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(x => x.Name.Contains(search));
			}
			return query.OrderByDescending(x => x.LastStartTime);

		}

		public WorkItemDetail GetDetail(Guid id)
		{
			var query = repository.Query<WorkItem>()
				.Where(wi => wi.Id == id)
				.Select(wi => new WorkItemDetail()
				              	{
				              		Id = wi.Id,
				              		Name = wi.Name,
				              		ProjectTaskTypeName = wi.ProjectTaskType.Name,
				              		StartTime = wi.TimeEntries.Where(te => te.EndTime == null)
											.Select(te => (DateTime?)te.StartTime)
											.FirstOrDefault()
				              	});
			return query.FirstOrDefault();
		}
	}
}
