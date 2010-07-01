using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;
using TimeTracker.Tasks;
using TimeTracker.Web.Models;

namespace TimeTracker.Web.Controllers
{
    public class ProjectTaskTypeController : BaseController
    {
    	private readonly IRepository repository;
    	private readonly IProjectTaskTypeTasks projectTaskTypeTasks;

    	public ProjectTaskTypeController(IRepository repository,
			IProjectTaskTypeTasks projectTaskTypeTasks)
    	{
			this.repository = repository;
    		this.projectTaskTypeTasks = projectTaskTypeTasks;
    	}

    	//
        // GET: /ProjectTaskType/

        public ActionResult Index()
        {
			  return View(projectTaskTypeTasks.GetAll());
        }


		 public ActionResult Create()
		 {
		 	var model = new CreateProjectTaskTypeView
			{
				Projects = repository.Query<Project>().ToList().Select(x =>
							  new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
				Tasks = repository.Query<Task>().ToList().Select(x =>
							  new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
				TaskTypes = repository.Query<TaskType>().ToList().Select(x =>
							  new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
			};
		 	return View(model);
		 }

		 [AcceptVerbs(HttpVerbs.Post)]
		 public ActionResult Create(CreateProjectTaskType createProjectTaskType)
		 {
			 if (!ModelState.IsValid)
			 {
			 	return Create();
			 }
			 projectTaskTypeTasks.Create(createProjectTaskType);
		 	return RedirectToAction("Index");
		 }
    }
}
