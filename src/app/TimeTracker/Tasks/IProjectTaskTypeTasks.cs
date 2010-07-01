using System.Linq;
using TimeTracker.DTO;

namespace TimeTracker.Tasks
{
	public interface IProjectTaskTypeTasks
	{
		void Create(CreateProjectTaskType createProjectTaskType);
		IQueryable<ProjectTaskTypeItem> GetAll();
	}
}