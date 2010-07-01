using System;
using System.Web.Mvc;
using TimeTracker.Tasks;

namespace TimeTracker.Web.Controllers
{
	public class ReportController : BaseController
	{
		private readonly IReportTasks reportTasks;

		public ReportController(IReportTasks reportTasks)
		{
			this.reportTasks = reportTasks;
		}

		public ActionResult LastWeek()
		{
			return View(reportTasks.GetReportForLastWeek());
		}

		public ActionResult ThisWeek()
		{
			return View("LastWeek", reportTasks.GetReportForThisWeek());
		}

		public ActionResult EmpowerxLastWeek()
		{
			return View("Weekly", reportTasks.GetEmpowerxReportForLastWeek());
		}

		public ActionResult Today()
		{
			return View(reportTasks.GetReportForToday());
		}

		public ActionResult Daily(DateTime day)
		{
			return View("Today", reportTasks.GetReportForDay(day));
		}

        public ActionResult Weekly(DateTime day, int numberOfDays = 7)
        {
            return View(reportTasks.GetReportForRange(day, numberOfDays));
        }
	}
}