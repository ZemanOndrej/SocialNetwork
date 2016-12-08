using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BL;
using Castle.Windsor;

namespace PL
{
    public class MvcApplication : HttpApplication
    {


		private static IWindsorContainer container;

		protected void Application_Start()
		{
			AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			BootstrapContainer();
		}

		private void BootstrapContainer()
		{
			container = new WindsorContainer();

			// configure DI            
			container.Install(new BLInstaller());
			container.Install(new MvcInstaller());

			// configure mapping within BL
			InitMapping.ConfigMapping();

			// initialize default account accounts (admin, ...)
//			UserAccountInit.InitializeUserAccounts(container);

			// set controller factory
			var controllerFactory = new WindsorControllerFactory(container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);
		}
	}
}
