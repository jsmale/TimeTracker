using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TimeTracker.Web.Models
{
	public class CreateProjectTaskTypeView
	{
		public IEnumerable<SelectListItem> Projects { get; set; }
		public IEnumerable<SelectListItem> Tasks { get; set; }
		public IEnumerable<SelectListItem> TaskTypes { get; set; }
	}
}