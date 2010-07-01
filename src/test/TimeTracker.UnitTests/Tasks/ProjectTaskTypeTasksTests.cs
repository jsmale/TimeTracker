using System;
using Machine.Specifications;
using Machine.Specifications.DevelopWithPassion.Rhino;
using Rhino.Mocks;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;
using TimeTracker.Tasks;

#if (NUNIT)
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace TimeTracker.UnitTests.Tasks
{
    public class WhenCreateRequestIsValid : Observes<ProjectTaskTypeTasks>
    {
        Establish e = () =>
        {
            createRequest = new CreateProjectTaskType
            {
                Name = "Name",
                ProjectId = Guid.NewGuid(),
                TaskId = Guid.NewGuid(),
                TaskTypeId = Guid.NewGuid()
            };
            repository = the_dependency<IRepository>();
        };

        Because b = () => sut.Create(createRequest);

        It shouldGetProjectFromProjectId = () =>
            repository.AssertWasCalled(x => x.Get<Project>(createRequest.ProjectId));

        It shouldGetTaskFromTaskId = () =>
            repository.AssertWasCalled(x => x.Get<Task>(createRequest.TaskId));

        static IRepository repository;
        static CreateProjectTaskType createRequest;
    }
	[TestClass]
	public class ProjectTaskTypeTasksTests
	{
		private CreateProjectTaskType createRequest;
		private IRepository repository;

		[TestInitialize]
		public void SetUp()
		{
			createRequest = new CreateProjectTaskType
			                	{
			                		Name = "Name",
			                		ProjectId = Guid.NewGuid(),
			                		TaskId = Guid.NewGuid(),
			                		TaskTypeId = Guid.NewGuid()
			                	};
			repository = MockRepository.GenerateMock<IRepository>();
		}

		IProjectTaskTypeTasks CreateSUT()
		{
			return new ProjectTaskTypeTasks(repository);
		}

		[TestMethod]
		[Description("Tasks")]
		public void Create_should_get_taskType_from_TaskTypeId()
		{
			CreateSUT().Create(createRequest);

			repository.AssertWasCalled(x => x.Get<TaskType>(createRequest.TaskTypeId));
		}

		[TestMethod]
		[Description("Tasks")]
		public void Create_should_insert_new_project_task_type()
		{
			var project = new Project();
			var task = new Task();
			var taskType = new TaskType();
			repository.Stub(x => x.Get<Project>(createRequest.ProjectId)).Return(project);
			repository.Stub(x => x.Get<Task>(createRequest.TaskId)).Return(task);
			repository.Stub(x => x.Get<TaskType>(createRequest.TaskTypeId)).Return(taskType);

			CreateSUT().Create(createRequest);

			repository.AssertWasCalled(x => x.Insert(Arg<ProjectTaskType>.Matches(y =>
				y.Name == createRequest.Name
				&& y.Project == project
				&& y.Task == task
				&& y.Type == taskType
				&& y.Id != Guid.Empty)));
		}
	}
}