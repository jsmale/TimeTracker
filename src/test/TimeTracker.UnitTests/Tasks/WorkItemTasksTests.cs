using System.Collections.Generic;
using Rhino.Mocks;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;
using TimeTracker.Tasks;
using System;

#if (NUNIT)
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

#endif

namespace TimeTracker.UnitTests.Tasks
{
	[TestClass]
	public class WorkItemTasksTests
	{
		private IRepository repository;
		private IUserTasks userTasks;
		private CreateWorkItemRequest createWorkItemRequest;
		private ProjectTaskType projectTaskType;
		private User user;
		private DateTime startTime1;
		private DateTime startTime2;

		[TestInitialize]
		public void SetUp()
		{
			repository = MockRepository.GenerateMock<IRepository>();
			userTasks = MockRepository.GenerateMock<IUserTasks>();
			projectTaskType = new ProjectTaskType();
			user = new User();

			this.createWorkItemRequest = new CreateWorkItemRequest()
			{
				Name = "Bond",
				ProjectTaskTypeId = Guid.NewGuid()
			};
			repository.Stub(x => x.Get<ProjectTaskType>(createWorkItemRequest.ProjectTaskTypeId)).Return(projectTaskType);
			repository.Stub(x => x.Query<WorkItem>()).Return((new WorkItem[0]).AsQueryable());
			userTasks.Stub(x => x.GetCurrentUser()).Return(user);
		}

		public IWorkItemTasks CreateSUT()
		{
			return new WorkItemTasks(repository,userTasks);
		}

		[TestMethod]
		public void Create_should_get_project_task_type_from_the_repository()
		{

			CreateSUT().Create(createWorkItemRequest);

			repository.AssertWasCalled(x => x.Get<ProjectTaskType>(createWorkItemRequest.ProjectTaskTypeId));
		}

		[TestMethod]
		public void Create_should_get_currently_login_user()
		{
			CreateSUT().Create(createWorkItemRequest);

			userTasks.AssertWasCalled(x => x.GetCurrentUser());
		}

		[TestMethod]
		public void Create_should_insert_work_item_into_repository()
		{
			CreateSUT().Create(createWorkItemRequest);

			repository.AssertWasCalled(
				y => y.Insert(Arg<WorkItem>.Matches(x => x.Id != Guid.Empty &&
					x.Name == createWorkItemRequest.Name &&
					x.ProjectTaskType == projectTaskType &&
					x.User == user))
			);
		}

		[TestMethod]
		public void Create_should_return_id_of_new_work_item()
		{
			var result = CreateSUT().Create(createWorkItemRequest);

			var workItem = repository.GetArgumentsForCallsMadeOn(x => x.Insert((WorkItem)null))[0][0] as WorkItem;
			
			Assert.AreNotEqual(workItem.Id, Guid.Empty);
			Assert.AreEqual(workItem.Id, result);
		}

		[TestMethod]
		public void GetWorkItems_should_get_the_current_user()
		{
			CreateSUT().GetWorkItems();
		
			userTasks.AssertWasCalled(x => x.GetCurrentUser());
		}

		[TestMethod]
		public void GetWorkItems_convert_and_return_current_users_work_items_from_repository()
		{
			WorkItem workItem1;
			WorkItem workItem3;
			SetupWorkItemList(out workItem1, out workItem3);

			var result = CreateSUT().GetWorkItems().ToList();

			Assert.AreEqual(2, result.Count());
			AssertWorkItemsAreEqual(result[0], workItem3);
			Assert.AreEqual(result[0].LastStartTime, DateTime.MaxValue);
			AssertWorkItemsAreEqual(result[1], workItem1);
			Assert.AreEqual(result[1].LastStartTime, startTime1);
		}

		[TestMethod]
		public void GetWorkItems_with_search_convert_and_return_current_users_work_items_from_repository()
		{
			WorkItem workItem1;
			WorkItem workItem3;
			SetupWorkItemList(out workItem1, out workItem3);

			var result = CreateSUT().GetWorkItems("Work Item 1").ToList();

			Assert.AreEqual(1, result.Count());
			AssertWorkItemsAreEqual(result[0], workItem1);
		}

		private void SetupWorkItemList(out WorkItem workItem1, out WorkItem workItem3)
		{
			var user2 = new User { Id = Guid.NewGuid(), Username = "user2" };
			workItem1 = new WorkItem
			{
				Id = Guid.NewGuid(),
				Name = "Work Item 1",
				ProjectTaskType = new ProjectTaskType
				{
					Name = "Task Type 1"
				},
				User = user
			};
			startTime1 = new DateTime(2010, 1, 1);
			workItem1.TimeEntries.Add(new TimeEntry { StartTime = new DateTime(2009, 1, 1) });
			workItem1.TimeEntries.Add(new TimeEntry { StartTime = startTime1 });
			var workItem2 = new WorkItem
			{
				Id = Guid.NewGuid(),
				Name = "Work Item 2",
				ProjectTaskType = new ProjectTaskType
				{
					Name = "Task Type 2"
				},
				User = user2
			};
			startTime2 = new DateTime(2010, 2, 1);
			workItem2.TimeEntries.Add(new TimeEntry { StartTime = startTime2 });
			workItem3 = new WorkItem
			{
				Id = Guid.NewGuid(),
				Name = "Work Item 3",
				ProjectTaskType = new ProjectTaskType
				{
					Name = "Task Type 3"
				},
				User = user
			};
			var workItems = new List<WorkItem>
			                {
			                	workItem1,
									workItem2,
									workItem3
			                };
			repository.Stub(x => x.Query<WorkItem>()).Return(workItems.AsQueryable()).Repeat.Any();
		}

		private void AssertWorkItemsAreEqual(WorkItemListItem result, WorkItem item)
		{
			Assert.AreEqual(item.Id, result.Id);
			Assert.AreEqual(item.Name, result.Name);
			Assert.AreEqual(item.ProjectTaskType.Name, result.ProjectTaskTypeName);
		}
	}
}