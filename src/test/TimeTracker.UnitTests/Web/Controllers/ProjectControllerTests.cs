using MvcContrib.TestHelper;
using Rhino.Mocks;
using TimeTracker.DTO;
using TimeTracker.Tasks;
using TimeTracker.Web.Controllers;

#if (NUNIT)
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace TimeTracker.UnitTests.Web.Controllers
{
	[TestClass]
	public class ProjectControllerTests
	{
		private TestControllerBuilder builder;
		private IProjectTasks projectTasks;

		[TestInitialize]
		public void SetUp()
		{
			builder = new TestControllerBuilder();
			projectTasks = MockRepository.GenerateMock<IProjectTasks>();
		}

		ProjectController CreateSUT()
		{
			return builder.CreateController<ProjectController>(projectTasks);
		}

		[TestMethod]
        [Description ("Controllers")]
		public void Create_should_redirect_to_index()
		{
			var project = new CreateProjectRequest { Name = "Name", ProjectCode = "Code" };

			var result = CreateSUT().Create(project);

			var redirectResult = result.AssertActionRedirect();
			Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
		}

		[TestMethod]
        [Description("Controllers")]
		public void Create_should_return_view_if_project_is_not_valid()
		{
			var project = new CreateProjectRequest { Name = null, ProjectCode = "Code" };

			var sut = CreateSUT();
			sut.ModelState.AddModelError("Name", "Name is required");
			var result = sut.Create(project);

			var viewResult = result.AssertViewRendered();
			Assert.AreEqual("", viewResult.ViewName);
		}

		[TestMethod]
        [Description("Controllers")]
		public void Create_should_delegate_to_project_tasks()
		{
			var project = new CreateProjectRequest { Name = "Name", ProjectCode = "Code" };

			CreateSUT().Create(project);

			projectTasks.AssertWasCalled(x => x.CreateProject(project));
		}
	}
}