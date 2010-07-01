using System.Collections.Generic;
using System.Web.Mvc;
using TimeTracker.DTO;

namespace TimeTracker.Web.Models
{
	public class CreateWorkItemView
	{
		public IEnumerable<SelectListItem> ProjectTaskTypes { get; set; }
		public CreateWorkItemRequest CreateWorkItemRequest { get; set; }
	}
}