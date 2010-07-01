using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;

namespace TimeTracker.Tasks
{
	public class ReportTasks : IReportTasks
	{
		private readonly IUserTasks userTasks;
		private readonly IRepository repository;
		private readonly IDateTimeTasks dateTimeTasks;

		public ReportTasks(IUserTasks userTasks, IRepository repository, IDateTimeTasks dateTimeTasks)
		{
			this.userTasks = userTasks;
			this.repository = repository;
			this.dateTimeTasks = dateTimeTasks;
		}

		public ReportOutput GetReportForLastWeek()
		{
			return GetWeeklyReport(dateTimeTasks.Now().Date.AddDays(-7));
		}

		public ReportOutput GetEmpowerxReportForLastWeek()
		{
            const int numberOfDays = 7;
            var startDate = dateTimeTasks.Now().Date.AddDays(-numberOfDays);
			startDate = startDate.AddDays(DayOfWeek.Wednesday - startDate.DayOfWeek);
		    return new ReportOutput
			{
				StartDate = startDate,
                NumberOfDays = numberOfDays,
                ReportDetails = GetReportDetailsForUser(startDate, numberOfDays, userTasks.GetCurrentUser(),
					x => x.Project.StartsWith("EMPOWERx"))
			};
		}

		public ReportOutput GetReportForToday()
		{
			var startDate = dateTimeTasks.Now().Date;
			return GetReportForDay(startDate);
		}

		public ReportOutput GetReportForThisWeek()
		{
			return GetWeeklyReport(dateTimeTasks.Now().Date);
		}

		public ReportOutput GetReportForDay(DateTime day)
		{
			return new ReportOutput
			{
				StartDate = day,
                NumberOfDays = 1,
				ReportDetails = GetReportDetailsForUser(day, 1, userTasks.GetCurrentUser(), null)
			};
		}

		private ReportOutput GetWeeklyReport(DateTime startDate)
		{
			startDate = startDate.AddDays(DayOfWeek.Monday - startDate.DayOfWeek);
		    const int numberOfDays = 7;
			return new ReportOutput
			       	{
			       		StartDate = startDate,
                        NumberOfDays = numberOfDays,
                        ReportDetails = GetReportDetailsForUser(startDate, numberOfDays, userTasks.GetCurrentUser())
			       	};
		}

        public ReportOutput GetReportForRange(DateTime startDate, int numberOfDays)
        {
            return new ReportOutput
            {
                StartDate = startDate,
                NumberOfDays = numberOfDays,
                ReportDetails = GetReportDetailsForUser(startDate, numberOfDays, userTasks.GetCurrentUser(), null)
            };
        }

		private IQueryable<ReportDetail> GetReportDetailsForUser(DateTime startDate, int numberOfDays, User user)
		{
			var endDate = startDate.AddDays(numberOfDays);
			var query = from x in
								(from te in repository.Query<TimeEntry>()
								 where te.WorkItem.User == user
										 && te.StartTime >= startDate
										 && te.StartTime < endDate
										 && te.EndTime != null
								 select new ReportTimeEntry
								 {
									 Project = te.WorkItem.ProjectTaskType.Project.ProjectCode,
									 Task = te.WorkItem.ProjectTaskType.Task.TaskCode,
									 Type = te.WorkItem.ProjectTaskType.Type.TypeCode,
									 StartTime = te.StartTime,
									 EndTime = te.EndTime.Value
								 })
							orderby x.Project, x.Task, x.Type, x.StartTime
							select x;
			return GetReportDetails(startDate, query, numberOfDays);
		}

		private static IQueryable<ReportDetail> GetReportDetails(DateTime startDate, IEnumerable<ReportTimeEntry> reportTimeEntries,
			int numberOfDays)
		{
			var results = new List<ReportDetail>();
			foreach (var item in reportTimeEntries)
			{
				var detail = results.FirstOrDefault(x =>
				                                    x.Project == item.Project &&
				                                    x.Task == item.Task &&
				                                    x.Type == item.Type
					);
				if (detail == null)
				{
					detail = new ReportDetail
					         	{
					         		Project = item.Project,
					         		Task = item.Task,
					         		Type = item.Type,
										Hours = new double[numberOfDays]
					         	};
					results.Add(detail);
				}
				var dayIndex = (item.StartTime.Date - startDate).Days;
				var hours = (item.EndTime - item.StartTime).TotalHours;
				detail.Hours[dayIndex] += hours;
			}
			return results.AsQueryable();
		}

		private class ReportTimeEntry
		{
			public string Project { get; set; }
			public string Task { get; set; }
			public string Type { get; set; }
			public DateTime StartTime { get; set; }
			public DateTime EndTime { get; set; }
		}

		private IQueryable<ReportDetail> GetReportDetailsForUser(DateTime startDate, int numberOfDays, User user, 
			Func<ReportTimeEntry, bool> filter)
		{
			var endDate = startDate.AddDays(numberOfDays);
			IEnumerable<ReportTimeEntry> query = from x in
								(from te in repository.Query<TimeEntry>()
								 where te.WorkItem.User == user
										 && te.StartTime >= startDate
										 && te.StartTime < endDate
										 && te.EndTime != null
								 select new ReportTimeEntry
								 {
									 Project = te.WorkItem.ProjectTaskType.Project.Name,
									 Task = te.WorkItem.Name,
									 Type = te.WorkItem.ProjectTaskType.Type.Name,
									 StartTime = te.StartTime,
									 EndTime = te.EndTime.Value
								 })
							orderby x.Project, x.Task, x.Type, x.StartTime
							select x;
			if (filter != null)
			{
				query = query.Where(filter);
			}
			return GetReportDetails(startDate, query, numberOfDays);
		}
	}
}