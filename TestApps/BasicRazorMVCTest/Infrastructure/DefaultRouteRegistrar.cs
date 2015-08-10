using System.Web.Mvc;
using System.Web.Routing;
using JBSolutions.Common;
using JBSolutions.Common.Web.Contracts;

namespace BasicRazorMVCTest.Infrastructure
{
    /// <summary>
    /// Registers the default routes.
    /// </summary>
    [ExportRegistrar]
    public class DefaultRouteRegistrar : IRouteRegistrar
    {
        #region Methods
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public void RegisterRoutes(RouteCollection routes)
        {
            Throw.IfArgumentNull(routes, "routes");

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ico/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
        #endregion

    }
}