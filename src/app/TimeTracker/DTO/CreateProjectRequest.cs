using Castle.Components.Validator;

namespace TimeTracker.DTO
{
	public class CreateProjectRequest
	{
		[ValidateNonEmpty("Project Name is required")]
		public virtual string Name { get; set; }
		[ValidateNonEmpty("Project Code is required")]
		public virtual string ProjectCode { get; set; }
	}
}