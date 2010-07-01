using System;

namespace TimeTracker.DTO
{
	public class WorkItemListItem
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string ProjectTaskTypeName { get; set; }
		public virtual DateTime LastStartTime { get; set; }
	}
}