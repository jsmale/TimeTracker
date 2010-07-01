using System;
using System.Collections.Generic;
using System.Linq;
using TimeTracker.DTO;

namespace TimeTracker.Tasks
{
	public interface IWorkItemTasks
	{
		Guid Create(CreateWorkItemRequest createWorkItemRequest);
		IQueryable<WorkItemListItem> GetWorkItems();
		IQueryable<WorkItemListItem> GetWorkItems(string search);
		WorkItemDetail GetDetail(Guid id);
	}
}