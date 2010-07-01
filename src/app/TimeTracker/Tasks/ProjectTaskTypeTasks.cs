using System;
using System.Linq;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;

namespace TimeTracker.Tasks
{
	public class ProjectTaskTypeTasks : IProjectTaskTypeTasks
	{
		private readonly IRepository repository;

		public ProjectTaskTypeTasks(IRepository repository)
		{
			this.repository = repository;
		}

		public void Create(CreateProjectTaskType createProjectTaskType)
		{
			var project = repository.Get<Project>(createProjectTaskType.ProjectId);
			var task = repository.Get<Task>(createProjectTaskType.TaskId);
			var taskType = repository.Get<TaskType>(createProjectTaskType.TaskTypeId);

			ProjectTaskType projectTaskType = new ProjectTaskType()
			                                  	{
			                                  		Id = Guid.NewGuid(),
			                                  		Name = createProjectTaskType.Name,
			                                  		Project = project,
			                                  		Task = task,
			                                  		Type = taskType
			                                  	};

			repository.Insert(projectTaskType);

		}

		public IQueryable<ProjectTaskTypeItem> GetAll()
		{
			return repository.Query<ProjectTaskType>().Select(x =>
			                                                new ProjectTaskTypeItem
			                                                	{
			                                                		Id = x.Id,
			                                                		Name = x.Name,
			                                                		Project = x.Project.Name,
			                                                		Task = x.Task.Name,
			                                                		TaskType = x.Type.Name
			                                                	});
		}
	}
}