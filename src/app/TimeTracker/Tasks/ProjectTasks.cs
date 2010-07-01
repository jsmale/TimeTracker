using System;
using System.Linq;
using System.Linq.Expressions;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;

namespace TimeTracker.Tasks
{
	public class ProjectTasks : IProjectTasks
	{
		private readonly IRepository<Project> repository;

//		ProjectResponse Convert(Project project)
//		{
//			return new ProjectResponse()
//			       	{
//			       		Id = project.Id,
//			       		Name = project.Name,
//			       		ProjectCode = project.ProjectCode
//			       	};
//		}

		private readonly Expression<Func<Project, ProjectResponse>> projectResponseConverter
			= project => new ProjectResponse
			             	{
				Id = project.Id,
				Name = project.Name,
				ProjectCode = project.ProjectCode
			};

		public ProjectTasks(IRepository<Project> repository)
		{
			this.repository = repository;
		}

		public void CreateProject(CreateProjectRequest createProjectRequest)
		{
			var project = new Project
			              	{
			              		Id = Guid.NewGuid(),
			              		Name = createProjectRequest.Name,
			              		ProjectCode = createProjectRequest.ProjectCode
			              	};
			repository.Insert(project);
		}

		public IQueryable<ProjectResponse> GetAllProjectResponses()
		{
			return repository.Query().Select(projectResponseConverter);
		}

		public ProjectResponse GetProjectResponse(Guid id)
		{
			var converter = projectResponseConverter.Compile();
			return converter.Invoke(repository.Get(id));
		}
		
		public void EditProject(Guid id, EditProjectRequest editProjectRequest)
		{
			var project = repository.Get(id);
			project.Name = editProjectRequest.Name;
			project.ProjectCode = editProjectRequest.ProjectCode;
			repository.SubmitChanges();
		}

		public void DeleteProject(Guid id)
		{
			var project = repository.Get(id);
			repository.Delete(project);
		}
	}
}