using System;

namespace TimeTracker.DTO
{
	public class WorkItemTimeEntryDetails
	{
		public Guid WorkItemId { get; set; }
		public DateTime? StartTime { get; set; }
	}
}