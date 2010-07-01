using System;

namespace TimeTracker.DTO
{
	public class ProjectTaskTypeItem
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Project { get; set; }
		public string Task { get; set; }
		public string TaskType { get; set; }
	}
}