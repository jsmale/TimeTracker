using System;
using System.Collections.Generic;
using TimeTracker.DTO;

namespace TimeTracker.Tasks
{
	public interface ITimeEntryTasks
	{
		WorkItemTimeEntryDetails StartWorkItem(Guid workItemId, DateTime? startTime);
		WorkItemTimeEntryDetails StopWorkItem(Guid workItemId, DateTime? stopTime);
		IEnumerable<TimeEntryDetail> GetTimeEntries(Guid workItemId);
	}
}