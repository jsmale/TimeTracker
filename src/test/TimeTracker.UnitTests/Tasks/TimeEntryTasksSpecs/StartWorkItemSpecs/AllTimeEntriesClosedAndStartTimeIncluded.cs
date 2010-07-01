using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;
using TimeTracker.Tasks;

namespace TimeTracker.UnitTests.Tasks.TimeEntryTasksSpecs.StartWorkItemSpecs
{
	[TestClass]
	public class AllTimeEntriesClosedAndStartTimeIncluded : AllTimeEntriesClosed
	{
		private DateTime startTime;

		protected override void Given()
		{
			base.Given();
			startTime = new DateTime(2009, 12, 1);
		}

		protected override void When()
		{
			result = CreateSut().StartWorkItem(id, startTime);
		}

		[TestMethod]
		public void ShouldAddTimeEntryToWorkItem()
		{
			GetMock<IRepository>().AssertWasCalled(x => x.Insert(
				Arg<TimeEntry>.Matches(y =>
					y.StartTime == startTime
					&& y.WorkItem == workItem
					&& y.EndTime == null
					&& y.Id != Guid.Empty
				)));
		}

		[TestMethod]
		public void ShouldReturnTimeEntryDetails()
		{
			Assert.AreEqual(startTime, result.StartTime);
			Assert.AreEqual(id, result.WorkItemId);
		}
	}
}