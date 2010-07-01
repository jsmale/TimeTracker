using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TimeTracker.Web.Code;

namespace TimeTracker.Web.Controllers
{
	[Authorize]
	[TimeTrackerHandleError]
    public abstract class BaseController : Controller
    {
    }
}
