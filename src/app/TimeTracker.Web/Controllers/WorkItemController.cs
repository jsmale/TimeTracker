using System;
using System.Web.Mvc;
using TimeTracker.DataAccess;
using TimeTracker.DTO;
using TimeTracker.Tasks;
using TimeTracker.Web.Models;
using TimeTracker.Domain;
using System.Linq;
using System.Linq.Dynamic;

namespace TimeTracker.Web.Controllers
{
	public class WorkItemController : BaseController
	{
		private readonly IRepository repository;
		private readonly IWorkItemTasks workItemTasks;
		private readonly ITimeEntryTasks timeEntryTasks;

		public WorkItemController(IRepository repository, IWorkItemTasks workItemTasks, ITimeEntryTasks timeEntryTasks)
		{
			this.repository = repository;
			this.workItemTasks = workItemTasks;
			this.timeEntryTasks = timeEntryTasks;
		}

		public ActionResult Index(string search, int? page, string sort)
		{
			ViewData["page"] = page ?? 1;
			sort = string.IsNullOrEmpty(sort) ? "LastStartTime DESC" : sort;
			ViewData["sort"] = sort;
			var items = workItemTasks.GetWorkItems(search);
			items = items.OrderBy(sort);
			return View(items);
		}

		public ActionResult Create()
		{
			var createWorkItemView =
				new CreateWorkItemView
				{
					ProjectTaskTypes = repository.Query<ProjectTaskType>().Select(
						x => new
						     {
						     	Text = x.Name,
						     	Value = x.Id
						     })
						.ToList()
						.Select(x =>
							new SelectListItem {Text = x.Text, Value = x.Value.ToString()}),
				};

			return View(createWorkItemView);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create(CreateWorkItemRequest createWorkItemRequest)
		{
			if (!ModelState.IsValid)
			{
				return Create();
			}

			Guid id = workItemTasks.Create(createWorkItemRequest);

			return RedirectToAction("Details", new {id});
		}

		public ActionResult Details(Guid id)
		{
			return View(workItemTasks.GetDetail(id));
		}

		public ActionResult Start(Guid id, DateTime? startTime)
		{
			return View("TimeEntryForm", timeEntryTasks.StartWorkItem(id, startTime));
		}

		public ActionResult Stop(Guid id, DateTime? stopTime)
		{
			return View("TimeEntryForm", timeEntryTasks.StopWorkItem(id, stopTime));
		}

		public ActionResult GetTimeEntries(Guid id)
		{
			return View(timeEntryTasks.GetTimeEntries(id));
		}
	}
}