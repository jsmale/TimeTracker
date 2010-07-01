using System;
using System.Collections.Generic;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;
using System.Linq;

namespace TimeTracker.Tasks
{
	public class TimeEntryTasks : ITimeEntryTasks
	{
		private readonly IRepository repository;
		private readonly IDateTimeTasks dateTimeTasks;

		public TimeEntryTasks(IRepository repository, IDateTimeTasks dateTimeTasks)
		{
			this.repository = repository;
			this.dateTimeTasks = dateTimeTasks;
		}

		public WorkItemTimeEntryDetails StartWorkItem(Guid workItemId, DateTime? startTime)
		{
			var workItem = repository.Query<WorkItem>().Where(x => x.Id == workItemId).FirstOrDefault();

			if(workItem.TimeEntries.Any(x => x.EndTime == null))
			{
				throw new Exception("Work item already started");
			}

			var timeEntry = new TimeEntry
			            	{
			            		Id = Guid.NewGuid(),
			            		StartTime = startTime??dateTimeTasks.Now(),
									WorkItem = workItem
			            	};

			repository.Insert(timeEntry);

			return new WorkItemTimeEntryDetails
			       	{
			       		StartTime = timeEntry.StartTime,
							WorkItemId = workItem.Id
			       	};
		}

		public WorkItemTimeEntryDetails StopWorkItem(Guid workItemId, DateTime? stopTime)
		{
			var workItem = repository.Query<WorkItem>().Where(x => x.Id == workItemId).FirstOrDefault();
			var timeEntry = workItem.TimeEntries.FirstOrDefault(x => x.EndTime == null);

			if (timeEntry == null)
			{
				throw new Exception("Work item not started");
			}
			if (timeEntry.StartTime > stopTime)
			{
				throw new Exception("Stop time may not be less than start time");
			}

			timeEntry.EndTime = stopTime ?? dateTimeTasks.Now();
			repository.SubmitChanges();

			return new WorkItemTimeEntryDetails
			{
				StartTime = null,
				WorkItemId = workItem.Id
			};
		}

		public IEnumerable<TimeEntryDetail> GetTimeEntries(Guid workItemId)
		{
			return from te in repository.Query<TimeEntry>()
			       where te.WorkItem.Id == workItemId
			             && te.EndTime != null
			       orderby te.StartTime descending
			       select new TimeEntryDetail
			              	{
			              		StartTime = te.StartTime,
			              		EndTime = (DateTime)te.EndTime
			              	};
		}
	}
}