using System;
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
	[TestClass]
	public class ProjectTasksTests
	{
		private IRepository<Project> repository;

		[TestInitialize]
		public void SetUp()
		{
			repository = MockRepository.GenerateMock<IRepository<Project>>();
		}

		ProjectTasks CreateSUT()
		{
			return new ProjectTasks(repository);
		}

		[TestMethod]
		[Description("Tasks")]
		public void Create_should_insert_project_into_repository()
		{
			var project = new CreateProjectRequest { Name = "Name", ProjectCode = "Code" };

			CreateSUT().CreateProject(project);

			repository.AssertWasCalled(x => x.Insert(Arg<Project>.Matches(y =>
				y.Id != Guid.Empty
				&& y.Name == project.Name
				&& y.ProjectCode == project.ProjectCode)));
		}
	}
}