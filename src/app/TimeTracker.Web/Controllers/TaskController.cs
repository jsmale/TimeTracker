using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using TimeTracker.DataAccess;
using TimeTracker.Domain;

namespace TimeTracker.Web.Controllers
{
    public class TaskController : Controller
    {
    	private readonly IRepository<Task> repository;

    	public TaskController(IRepository<Task> repository)
    	{
    		this.repository = repository;
    	}

    	//
        // GET: /Task/

        public ActionResult Index()
        {
			  return View(repository.Query());
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(Guid id)
        {
            return View(repository.Get(id));
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Task/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude="Id")] Task task)
        {
            try
            {
            	Validate(task);
					if (!ModelState.IsValid)
					{
						return View();
					}

            	task.Id = Guid.NewGuid();
					repository.Insert(task);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    	private void Validate(Task task)
    	{
    		if (string.IsNullOrEmpty(task.Name))
    		{
    			ModelState.AddModelError("Name", "Name is required");
    		}
			if (string.IsNullOrEmpty(task.TaskCode))
			{
				ModelState.AddModelError("TaskCode", "Task Code is required");
			}
    	}

    	//
        // GET: /Task/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            return View(repository.Get(id));
        }

        //
        // POST: /Task/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Guid id, [Bind(Exclude="Id")] Task task)
        {
            try
            {
            	Validate(task);
					if (!ModelState.IsValid)
					{
						return View();
					}

            	var existingTask = repository.Get(id);
            	existingTask.Name = task.Name;
            	existingTask.TaskCode = task.TaskCode;
					repository.SubmitChanges();
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
