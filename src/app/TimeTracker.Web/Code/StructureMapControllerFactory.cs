using System;
using System.Web.Mvc;
using StructureMap;
using System.Web.Routing;

namespace TimeTracker.Web.Code
{
	public class StructureMapControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				return null;
			}
			return (IController)ObjectFactory.GetInstance(controllerType);
		}
	}
}