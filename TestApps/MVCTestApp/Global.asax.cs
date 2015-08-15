using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using JBSolutions.Common;
using JBSolutions.Common.Extensions;
using JBSolutions.Common.Web;
using Microsoft.Practices.Unity;

namespace MVCTestApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : Application //System.Web.HttpApplication
    {
        #region Fields
        private IUnityContainer unityContainer;
        private CSLExportProvider exportProvider;
        #endregion

        #region Methods
        /// <summary>
        /// Creates the instance of the Unity container.
        /// </summary>
        protected override void PreCompose()
        {
            unityContainer = new UnityContainer();

            var locator = new UnityServiceLocator(unityContainer);
            exportProvider = new CSLExportProvider(locator);

            unityContainer.AddExtension(new CSLExportProviderExtension(exportProvider));

            RegisterTypes();
        }

        /// <summary>
        /// Registers any required types for the Unity container.
        /// </summary>
        protected void RegisterTypes()
        {
            //unityContainer.RegisterType<ITicketSystem, SimpleTicketSystem>();
        }

        /// <summary>
        /// Creates the composer used for composition.
        /// </summary>
        /// <returns></returns>
        protected override JBSolutions.Common.Composition.Composer CreateComposer()
        {
            var composer = base.CreateComposer();
            composer.AddExportProvider(exportProvider);

            return composer;
        }

        /// <summary>
        /// Initialise MVC Application
        /// </summary>
        protected override void Initialise()
        {
            base.Initialise();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
        #endregion

        #region Default MVC Application_Start Method
        /*
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
        */
        #endregion Default MVC Application_Start Method
    }
}