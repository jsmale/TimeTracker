using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.DTO
{
	public class CreateWorkItemRequest
	{
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Project Task Type is required")]
		public Guid ProjectTaskTypeId { get; set; }
	}
}