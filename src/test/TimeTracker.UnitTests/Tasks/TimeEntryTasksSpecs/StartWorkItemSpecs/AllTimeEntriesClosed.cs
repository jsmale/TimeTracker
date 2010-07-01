using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Rhino.Mocks;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;
using TimeTracker.Tasks;

namespace TimeTracker.UnitTests.Tasks.TimeEntryTasksSpecs.StartWorkItemSpecs
{
	public abstract class AllTimeEntriesClosed : SpecificationBase<TimeEntryTasks, ITimeEntryTasks>
	{
		protected Guid id;
		protected WorkItem workItem;
		protected WorkItemTimeEntryDetails result;

		protected override void Given()
		{
			id = Guid.NewGuid();
			workItem = new WorkItem
			           	{
			           		Id = id,
			           		TimeEntries = new EntitySet<TimeEntry>
			           		              	{
			           		              		new TimeEntry
			           		              			{
			           		              				StartTime = new DateTime(2000,1,1),
			           		              				EndTime = new DateTime(2000,1,1,1,0,0)
			           		              			}
			           		              	}
			           	};
			GetMock<IRepository>()
				.Stub(x => x.Query<WorkItem>())
				.Return((new[] { workItem }).AsQueryable());
		}
	}
}