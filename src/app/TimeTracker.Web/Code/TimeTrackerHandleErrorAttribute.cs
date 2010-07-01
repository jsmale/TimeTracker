using System;
using System.Web.Mvc;

namespace TimeTracker.Web.Code
{
	public class TimeTrackerHandleErrorAttribute : HandleErrorAttribute
	{
		public override void OnException(ExceptionContext filterContext)
		{
			filterContext.Controller.TempData["Error"] = filterContext.Exception.Message;

			base.OnException(filterContext);
		}
	}
}