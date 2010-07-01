using System;
using System.Collections.Generic;
using System.Data.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;
using TimeTracker.Tasks;
using Rhino.Mocks;
using System.Linq;

namespace TimeTracker.UnitTests.Tasks.TimeEntryTasksSpecs.StartWorkItemSpecs
{
	[TestClass]
	public class TimeEntryNotClosed : SpecificationBase<TimeEntryTasks, ITimeEntryTasks>
	{
		private Guid id;
		private WorkItem workItem;
		private WorkItemTimeEntryDetails result;
		private Exception exceptionOccured;

		protected override void Given()
		{
			id = Guid.NewGuid();
			workItem = new WorkItem
			           	{
			           		Id = id,
			           		TimeEntries = new EntitySet<TimeEntry>
			           		              	{
			           		              		new TimeEntry
			           		              			{
			           		              				StartTime = new DateTime(2000, 1, 1),
			           		              				EndTime = null
                                      			}
                                      	}
			           	};
			GetMock<IRepository>()
				.Stub(x => x.Query<WorkItem>())
				.Return((new[] {workItem}).AsQueryable());
		}

		protected override void When()
		{
			try
			{
				result = CreateSut().StartWorkItem(id, null);
			}
			catch (Exception ex)
			{
				exceptionOccured = ex;
			}
		}


		[TestMethod]
		public void ShouldThrowException()
		{
			Assert.IsNotNull(exceptionOccured);
			Assert.AreEqual("Work item already started", exceptionOccured.Message);
		}
	}
}