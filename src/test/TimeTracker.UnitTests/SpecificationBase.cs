using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using Rhino.Mocks;

#if (NUNIT)
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

#endif

namespace TimeTracker.UnitTests
{
	public abstract class ControllerSpecificationBase<T> : SpecificationBase where T : Controller
	{
		private TestControllerBuilder builder;

		protected override void PreSetUp()
		{
			base.PreSetUp();
			builder = new TestControllerBuilder();
		}

		protected T CreateSut()
		{
			return builder.CreateController<T>(CreateConstructorArguments(typeof(T)));
		}
	}

	public abstract class SpecificationBase
	{
		private IDictionary<Type, object> dictionary;
		private IDictionary<string, object> constructorArgs;

		protected virtual void AddConstructorArg(string parameterName, object value)
		{
			constructorArgs.Add(parameterName, value);
		}

		private static ParameterInfo[] GetGreedyContructorParameterInfos(Type type)
		{
			ParameterInfo[] parameterInfos = null;
			foreach (ConstructorInfo constructorInfo in type.GetConstructors())
			{
				ParameterInfo[] parameterInfosCompare = constructorInfo.GetParameters();
				if (parameterInfos == null || parameterInfos.Length < parameterInfosCompare.Length)
				{
					parameterInfos = parameterInfosCompare;
				}
			}
			return parameterInfos;
		}

		protected object[] CreateConstructorArguments(Type type)
		{
			var args = new List<object>();
			ParameterInfo[] parameterInfos = GetGreedyContructorParameterInfos(type);
			if (parameterInfos != null)
			{
				foreach (ParameterInfo parameterInfo in parameterInfos)
				{
					Type pType = parameterInfo.ParameterType;
					string parameterName = parameterInfo.Name;
					if (constructorArgs.ContainsKey(parameterName))
					{
						args.Add(constructorArgs[parameterName]);
					}
					else if (pType.IsInterface || pType.IsClass)
					{
						object mockObject = GetMock(pType);
						args.Add(mockObject);
					}
					else
					{
						throw new Exception("this type has constructor arguments that cannot be mocked");
					}
				}
			}
			return args.ToArray();
		}

		private object GetMock(Type type)
		{
			if (dictionary.ContainsKey(type))
			{
				return dictionary[type];
			}

			var mock = CreateMock(type);
			dictionary.Add(type, mock);
			return mock;
		}

		private static object CreateMock(Type type)
		{
			var methods = typeof(MockRepository).GetMethods(BindingFlags.Static | BindingFlags.Public);
		    var createMethod = methods.First(x => x.Name == "GenerateMock");
			var specificCreateMethod = createMethod.MakeGenericMethod(new[] { type });
			return specificCreateMethod.Invoke(null, new[] { new object[0] });
		}

		protected TMock GetMock<TMock>()
		{
			return (TMock)GetMock(typeof(TMock));
		}

		

		[TestInitialize]
		public void SetUp()
		{
			PreSetUp();
			Given();
			When();
		}

		protected virtual void PreSetUp()
		{
			dictionary = new Dictionary<Type, object>();
			constructorArgs = new Dictionary<string, object>();
		}

		protected abstract void Given();
		protected abstract void When();
	}

	public abstract class SpecificationBase<T> : SpecificationBase where T : class
	{

		protected virtual T CreateSut()
		{
			return MockRepository.GenerateStub<T>(CreateConstructorArguments(typeof(T)));
		}
	}

	public abstract class SpecificationBase<T, TInterface> : SpecificationBase<T> where T : class, TInterface
	{
		protected new virtual TInterface CreateSut()
		{
			return base.CreateSut();
		}
	}
}