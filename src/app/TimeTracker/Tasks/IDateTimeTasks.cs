using System;

namespace TimeTracker.Tasks
{
	public interface IDateTimeTasks
	{
		DateTime Now();
	}

	public class DateTimeTasks : IDateTimeTasks
	{
		public DateTime Now()
		{
			return DateTime.UtcNow;
		}
	}
}