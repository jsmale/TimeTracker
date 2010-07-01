using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;
using TimeTracker.Tasks;
using Rhino.Mocks;
using System.Linq;

namespace TimeTracker.UnitTests.Tasks.ReportTasksSpecs.LastWeekSpecs
{
	[TestClass]
	public class UserLoggedIn : SpecificationBase<ReportTasks, IReportTasks>
	{
		private User user;
		private ReportOutput result;

		protected override void Given()
		{
			user = new User();
			GetMock<IUserTasks>().Stub(x => x.GetCurrentUser())
				.Return(user);
			GetMock<IDateTimeTasks>().Stub(x => x.Now())
				.Return(new DateTime(2010, 1, 26));

			var timeEntries = new List<TimeEntry>()
			                  	{
			                  		new TimeEntry
			                  			{
			                  				StartTime = new DateTime(2010, 1, 21, 8, 0, 0),
			                  				EndTime = new DateTime(2010, 1, 21, 11, 0, 0),
			                  				WorkItem = new WorkItem
			                  				           	{
			                  				           		ProjectTaskType = new ProjectTaskType
			                  				           		                  	{
			                  				           		                  		Project = new Project {ProjectCode = "PCode"},
			                  				           		                  		Task = new Task {TaskCode = "TCode"},
			                  				           		                  		Type = new TaskType {TypeCode = "TypeCode"}
			                  				           		                  	},
			                  				           		User = user
			                  				           	}
			                  			}
			                  	};
			GetMock<IRepository>().Stub(x => x.Query<TimeEntry>())
				.Return(timeEntries.AsQueryable());
		}

		protected override void When()
		{
			result = CreateSut().GetReportForLastWeek();
		}

		[TestMethod]
		public void ShouldOutputTimeEntriesAsReportOutput()
		{
			Assert.AreEqual(new DateTime(2010, 1, 18), result.StartDate);
			Assert.AreEqual(1, result.ReportDetails.Count());
			var reportDetail = result.ReportDetails.ElementAt(0);
			for (var i = 0; i < 7; i++)
			{
				if (i != 3)
					Assert.AreEqual(0, reportDetail.Hours[i]);
			}

			Assert.AreEqual(3, reportDetail.Hours[3]);
			Assert.AreEqual("PCode", reportDetail.Project);
			Assert.AreEqual("TCode", reportDetail.Task);
			Assert.AreEqual("TypeCode", reportDetail.Type);
		}
	}
}