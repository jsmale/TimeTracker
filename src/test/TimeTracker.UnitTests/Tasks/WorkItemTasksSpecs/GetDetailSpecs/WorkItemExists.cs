using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.DTO;
using TimeTracker.Tasks;
using Rhino.Mocks;
using System.Linq;

namespace TimeTracker.UnitTests.Tasks.WorkItemTasksSpecs.GetDetailSpecs
{
	[TestClass]
	public class WorkItemExists : SpecificationBase<WorkItemTasks, IWorkItemTasks>
	{
		private Guid id;
		private WorkItem workItem;
		private WorkItemDetail result;

		protected override void Given()
		{
			id = Guid.NewGuid();
			workItem = new WorkItem
			           {
			           	Id = id, 
						Name = "WorkItem",
                        ProjectTaskType = new ProjectTaskType
                                          {
                                          	Name = "ProjectTaskType"
                                          }
			           };
			GetMock<IRepository>().Stub(x => x.Query<WorkItem>())
				.Return(new []{workItem}.AsQueryable());
		}

		protected override void When()
		{
			result = CreateSut().GetDetail(id);
		}

		[TestMethod]
		public void convert_work_item_into_work_item_detail()
		{
			Assert.AreEqual(workItem.Id, result.Id);
			Assert.AreEqual(workItem.Name, result.Name);
			Assert.AreEqual(workItem.ProjectTaskType.Name, result.ProjectTaskTypeName);
		}
	}
}