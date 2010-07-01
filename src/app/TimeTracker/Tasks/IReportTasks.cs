using System;
using TimeTracker.DTO;

namespace TimeTracker.Tasks
{
	public interface IReportTasks
	{
		ReportOutput GetReportForLastWeek();
		ReportOutput GetEmpowerxReportForLastWeek();
		ReportOutput GetReportForToday();
		ReportOutput GetReportForThisWeek();
		ReportOutput GetReportForDay(DateTime day);
	    ReportOutput GetReportForRange(DateTime startDate, int numberOfDays);
	}
}