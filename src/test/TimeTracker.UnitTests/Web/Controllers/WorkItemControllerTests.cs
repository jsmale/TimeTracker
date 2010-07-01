using System;
using System.Collections.Generic;
using MvcContrib.TestHelper;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;
using TimeTracker.Tasks;
using TimeTracker.Web.Controllers;
using System.Linq;
using TimeTracker.Web.Models;

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
	public class WorkItemControllerTests : ControllerSpecificationBase<WorkItemController>
	{
	    private IQueryable<WorkItemListItem> workItems;

	    protected override void Given()
        {
            workItems = new List<WorkItemListItem>().AsQueryable();
            GetMock<IWorkItemTasks>().Stub(x => x.GetWorkItems(Arg<string>.Is.Anything)).Return(workItems);
		}

		protected override void When()
		{
		}

		[TestMethod]
        [Description("Controllers")]
		public void CreateShouldGetListOfProjectTaskTypesFromRepository()
		{
			var projectTaskTypes = new List<ProjectTaskType>
        	{
				new ProjectTaskType
				{
					Id = Guid.NewGuid(),
               Name = "Task 1"
				},
				new ProjectTaskType
				{
					Id = Guid.NewGuid(),
               Name = "Task 2"
				}
        	};
			GetMock<IRepository>().Stub(x => x.Query<ProjectTaskType>()).Return(projectTaskTypes.AsQueryable());

			var result = CreateSut().Create();

			var viewResult = result.AssertViewRendered();
			var viewData = viewResult.ViewData.Model as CreateWorkItemView;
			var selectList = viewData.ProjectTaskTypes.ToList();
			for (int i = 0; i < projectTaskTypes.Count; i++)
			{
				Assert.AreEqual(projectTaskTypes[i].Id.ToString(), selectList[i].Value);
				Assert.AreEqual(projectTaskTypes[i].Name, selectList[i].Text);
			}
		}

		[TestMethod]
		public void Index_should_get_work_items_from_WorkItemTasks()
		{
			CreateSut().Index(null, null, null);

			GetMock<IWorkItemTasks>().AssertWasCalled(x => x.GetWorkItems(null));
		}

		[TestMethod]
		public void Index_should_return_work_items_in_view()
		{
			var result = CreateSut().Index(null, null, null);

			var view = result.AssertViewRendered();
            Assert.IsInstanceOfType(view.ViewData.Model, typeof(IQueryable<WorkItemListItem>));
		}

		[TestMethod]
		public void Detail_should_get_work_item_detail_from_WorkItemTasks()
		{
			var id = Guid.NewGuid();
			CreateSut().Details(id);

			GetMock<IWorkItemTasks>().AssertWasCalled(x => x.GetDetail(id));
		}

		[TestMethod]
		public void Detail_should_return_work_item_detail_in_the_view_data()
		{
			var id = Guid.NewGuid();
			var workItemDetail = new WorkItemDetail();
			GetMock<IWorkItemTasks>().Stub(x => x.GetDetail(id)).Return(workItemDetail);

			var viewResult = CreateSut().Details(id).AssertViewRendered();
			Assert.AreEqual(workItemDetail, viewResult.ViewData.Model);
		}

		[TestMethod]
		public void Start_should_start_work_item_on_TimeEntryTasks()
		{
			var id = Guid.NewGuid();
			var startTime = new DateTime(2000, 1, 1);

			CreateSut().Start(id, startTime);

			GetMock<ITimeEntryTasks>().AssertWasCalled(x => x.StartWorkItem(id, startTime));
		}

		[TestMethod]
		public void Start_should_return_work_item_time_entry_details_in_view()
		{
			var id = Guid.NewGuid();
			var startTime = new DateTime(2000, 1, 1);
			var workItemDetails = new WorkItemTimeEntryDetails();
			GetMock<ITimeEntryTasks>().Stub(x => x.StartWorkItem(id, startTime)).Return(workItemDetails);

			var rendered = CreateSut().Start(id, startTime).AssertViewRendered();

			Assert.AreEqual("TimeEntryForm", rendered.ViewName);
			Assert.AreEqual(workItemDetails, rendered.ViewData.Model);
		}
	}
}