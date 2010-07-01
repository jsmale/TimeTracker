using System;
using System.Web.Mvc;
using TimeTracker.DTO;
using TimeTracker.Tasks;

namespace TimeTracker.Web.Controllers
{
    public class ProjectController : BaseController
    {
    	private readonly IProjectTasks projectTasks;

    	public ProjectController(IProjectTasks projectTasks)
		{
			this.projectTasks = projectTasks;
		}

    	//
        // GET: /Project/

        public ActionResult Index()
        {
            return View(projectTasks.GetAllProjectResponses());
        }

        //
        // GET: /Project/Details/5

        public ActionResult Details(Guid id)
        {
			  return View(projectTasks.GetProjectResponse(id));
        }

        //
        // GET: /Project/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Project/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(CreateProjectRequest createProjectRequest)
        {
         	if (!ModelState.IsValid)
             {
             	return View();
             }

				projectTasks.CreateProject(createProjectRequest);

				return RedirectToAction("Index");
        }

    	//
        // GET: /Project/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            return Details(id);
        }

        //
        // POST: /Project/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
		  public ActionResult Edit(Guid id, EditProjectRequest editProjectRequest)
        {
            if (!ModelState.IsValid)
				{
					return Details(id);
				}

        		projectTasks.EditProject(id, editProjectRequest);

            return RedirectToAction("Index");
		  }


		  public ActionResult Delete(Guid id)
		  {
			  projectTasks.DeleteProject(id);

			  return RedirectToAction("Index");
		  }
    }
}
