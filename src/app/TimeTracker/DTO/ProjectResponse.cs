using System;

namespace TimeTracker.DTO
{
	public class ProjectResponse
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string ProjectCode { get; set; }
	}
}