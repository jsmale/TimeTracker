using System;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;
using StructureMap.Attributes;
using TimeTracker.DataAccess;
using TimeTracker.Domain;
using TimeTracker.Web.Code;
using TimeTracker.Web.Controllers;

namespace TimeTracker.Web
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			RegisterRoutes(RouteTable.Routes);
			RegisterContainer();
			RegisterModelBinders(ModelBinders.Binders);
		}

		
		static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
				);
		}

		private const string DataContextKey = "DataContextKey";
		DataContext CreateDataContext()
		{
			var context = new LinqToSqlDataContext();
			HttpContext.Current.Items[DataContextKey] = context;
			return context;
		}

		protected void Application_EndRequest(object sender, EventArgs e)
		{
			if (!Context.Items.Contains(DataContextKey))
			{
				return;
			}
			var context = (DataContext)Context.Items[DataContextKey];
			context.SubmitChanges();
		}
		
		void RegisterContainer()
		{
			ControllerBuilder.Current.SetControllerFactory(typeof (StructureMapControllerFactory));
			ObjectFactory.Initialize(x =>
       	{
       		x.ForRequestedType<DataContext>()
       			.CacheBy(InstanceScope.HttpContext)
					.TheDefault.Is.ConstructedBy(CreateDataContext);

       		x.ForRequestedType(typeof (IRepository<>))
       			.TheDefaultIsConcreteType(typeof (Repository<>));

				x.ForRequestedType<IRepository>()
					.TheDefaultIsConcreteType<LinqToSqlRepository>();

				x.ForRequestedType<IMembershipService>()
					.TheDefaultIsConcreteType<AccountMembershipService>();

       		x.Scan(y =>
	       	{
	       		y.AssemblyContainingType(typeof(Project));
	       		y.TheCallingAssembly();
	       		y.WithDefaultConventions();
	       	});
       	});
		}

		static void RegisterModelBinders(ModelBinderDictionary binders)
		{
			//binders.DefaultBinder = new ValidatingModelBinder();
		}
	}
}