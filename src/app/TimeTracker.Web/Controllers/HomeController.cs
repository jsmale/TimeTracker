using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeTracker.Web.Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewData["Message"] = "Welcome to ASP.NET MVC!";
			
			return View();
		}

		[Authorize]
		public ActionResult About()
		{
			return View();
		}

		public ActionResult TestTempData()
		{
			TempData["temp"] = "Temp Data";
			return RedirectToAction("RedirectHome");
		}

		public ActionResult RedirectHome()
		{
			return RedirectToAction("Index");
		}
	}
}
