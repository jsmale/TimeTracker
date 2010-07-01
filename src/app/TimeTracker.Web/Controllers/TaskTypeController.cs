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
    public class TaskTypeController : Controller
    {
    	private readonly IRepository<TaskType> repository;

    	public TaskTypeController(IRepository<TaskType> repository)
    	{
    		this.repository = repository;
    	}

    	//
        // GET: /TaskType/

        public ActionResult Index()
        {
            return View(repository.Query());
        }

        //
        // GET: /TaskType/Details/5

        public ActionResult Details(Guid id)
        {
            return View(repository.Get(id));
        }

        //
        // GET: /TaskType/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /TaskType/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude="Id")]TaskType taskType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                	return View();
                }

            	taskType.Id = Guid.NewGuid();
					repository.Insert(taskType);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /TaskType/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            return Details(id);
        }

        //
        // POST: /TaskType/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
		  public ActionResult Edit(Guid id, [Bind(Exclude = "Id")]TaskType taskType)
        {
            try
				{
					if (!ModelState.IsValid)
					{
						return Details(id);
					}

					var oldTaskType = repository.Get(id);
					oldTaskType.Name = taskType.Name;
					oldTaskType.TypeCode = taskType.TypeCode;
					repository.SubmitChanges();
 
                return RedirectToAction("Index");
            }
            catch
            {
					return Details(id);
            }
        }
    }
}
