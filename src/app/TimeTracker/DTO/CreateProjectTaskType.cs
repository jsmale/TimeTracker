using System;
using Castle.Components.Validator;

namespace TimeTracker.DTO
{
	public class CreateProjectTaskType
	{
		[ValidateNonEmpty("Name is required")]
		public string Name { get; set; }
		[ValidateNonEmpty("Project is required")]
		public Guid ProjectId { get; set; }
		[ValidateNonEmpty("Task is required")]
		public Guid TaskId { get; set; }
		[ValidateNonEmpty("Task Type is required")]
		public Guid TaskTypeId { get; set; }
	}
}