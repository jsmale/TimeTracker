using System;
using System.Collections.Generic;
using System.Linq;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.Tasks;
using Rhino.Mocks;

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
	public class UserExistsInRepository : SpecificationBase<UserTasks>
	{
		private string userName;
		private IList<User> users;
		private User result;
		private User user;

		protected override void Given()
		{
			userName = "userName";
			user = new User { Username = userName };
			users = new List<User>
			        {
						  new User{Username = "blah"},
							user
			        };
			GetMock<IRepository>().Stub(x => x.Query<User>()).Return(users.AsQueryable());
		}

		protected override void When()
		{
			var sut = CreateSut();
			sut.Stub(x => x.GetCurrentUserName()).Return(userName);
			result = sut.GetCurrentUser();
		}

		[TestMethod]
		public void ShouldReturnUserFromRepository()
		{
			Assert.AreEqual(user, result);
		}
	}
}