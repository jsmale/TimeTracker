using System;
using System.Collections.Generic;
using Rhino.Mocks;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.Tasks;
using System.Linq;

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
	public class UserDoesNotExistInRepository : SpecificationBase<UserTasks>
	{
		private string userName;
		private User result;

		protected override void Given()
		{
			userName = "userName";
			GetMock<IRepository>().Stub(x => x.Query<User>()).Return(new List<User>().AsQueryable());
		}

		protected override void When()
		{
			var sut = CreateSut();
			sut.Stub(x => x.GetCurrentUserName()).Return(userName);
			result = sut.GetCurrentUser();
		}

		[TestMethod]
		public void ShouldInsertNewUserToRepository()
		{
			GetMock<IRepository>().AssertWasCalled(x => x.Insert(Arg<User>.Matches(
				y => y.Username == userName && y.Id != Guid.Empty)));
		}

		[TestMethod]
		public void ShouldReturnNewUser()
		{
			var user = GetMock<IRepository>().GetArgumentsForCallsMadeOn(
			           	x => x.Insert<User>(null))[0][0] as User;
			Assert.AreEqual(user, result);
		}
	}
}