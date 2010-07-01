using System;
using Rhino.Mocks;
using TimeTracker.Domain;
using TimeTracker.Tasks;

#if (NUNIT)
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace TimeTracker.UnitTests.Tasks.UserTasksTests
{
	[TestClass]
	public class UserIsNotAuthenticated : SpecificationBase<UserTasks>
	{
		private string userName;
		private User result;

		protected override void Given()
		{
			userName = "";
		}

		protected override void When()
		{
			var sut = CreateSut();
			sut.Stub(x => x.GetCurrentUserName()).Return(userName);
			result = sut.GetCurrentUser();
		}

		[TestMethod]
		public void ShouldReturnNull()
		{
			Assert.IsNull(result);
		}
	}
}