using System;

namespace TimeTracker.DTO
{
	public class WorkItemDetail
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string ProjectTaskTypeName { get; set; }
		public DateTime? StartTime { get; set; }
	}
}