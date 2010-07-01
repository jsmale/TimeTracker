using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;
using TimeTracker.Tasks;
using Rhino.Mocks;

namespace TimeTracker.UnitTests.Tasks.TimeEntryTasksSpecs.StopWorkItemSpecs
{
	[TestClass]
	public class StopTimeIsLessThanStartTime : SpecificationBase<TimeEntryTasks, ITimeEntryTasks>
	{
		private DateTime stopTime;
		private Guid workItemId;
		private WorkItemTimeEntryDetails result;
		private Exception thrown;

		protected override void Given()
		{
			result = null;
			thrown = null;

			workItemId = Guid.NewGuid();
			var startTime = new DateTime(2010, 1, 2);
			stopTime = new DateTime(2010, 1, 1);
			IList<WorkItem> workItems = new List<WorkItem>
       	{
       		new WorkItem
    			{
    				Id = workItemId,
               TimeEntries = new EntitySet<TimeEntry>
              	{
              		new TimeEntry{StartTime = startTime}
              	}
    			}
       	};
			GetMock<IRepository>().Stub(x => x.Query<WorkItem>())
				.Return(workItems.AsQueryable());
		}

		protected override void When()
		{
			try
			{
				result = CreateSut().StopWorkItem(workItemId, stopTime);
			}
			catch(Exception ex)
			{
				thrown = ex;
			}
		}

		[TestMethod]
		public void ShouldThrowException()
		{
			Assert.IsNotNull(thrown);
			Assert.AreEqual("Stop time may not be less than start time", thrown.Message);
		}
	}
}