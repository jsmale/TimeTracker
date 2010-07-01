using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.Tasks;
using Rhino.Mocks;

namespace TimeTracker.UnitTests.Tasks.TimeEntryTasksSpecs.StartWorkItemSpecs
{
	[TestClass]
	public class AllTimeEntriesClosedAndStartTimeNotIncluded : AllTimeEntriesClosed
	{
		private DateTime now;

		protected override void Given()
		{
			base.Given();
			now = new DateTime(2009,12,1);
			GetMock<IDateTimeTasks>().Stub(x => x.Now()).Return(now);
		}

		protected override void When()
		{
			result = CreateSut().StartWorkItem(id, null);
		}

		[TestMethod]
		public void TimeEntryShouldBeAddedWithCurrentTime()
		{
			GetMock<IRepository>().AssertWasCalled(x => x.Insert(
				Arg<TimeEntry>.Matches(y => 
					y.StartTime == now
					&& y.WorkItem == workItem
					&& y.EndTime == null
					&& y.Id != Guid.Empty
				)));
		}

		[TestMethod]
		public void ShouldReturnTimeEntryWithCurrentTime()
		{
			Assert.AreEqual(now, result.StartTime);
		}
	}
}