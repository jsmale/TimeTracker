using System;
using System.Linq;
using TimeTracker.DTO;

namespace TimeTracker.Tasks
{
	public interface IProjectTasks
	{
		void CreateProject(CreateProjectRequest createProjectRequest);
		IQueryable<ProjectResponse> GetAllProjectResponses();
		ProjectResponse GetProjectResponse(Guid id);
		void EditProject(Guid id, EditProjectRequest editProjectRequest);
		void DeleteProject(Guid id);
	}

}