using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Routing;
using JBSolutions.Common.Web.Contracts.Routing;

namespace BasicRazorMVCTest.Infrastructure
{
    /// <summary>
    /// Registers the default MVC routes.
    /// </summary>
    [Export(typeof(IRouteRegistrar)), ExportMetadata("Order", 100)]
    public class DefaultRouteRegistrar : IRouteRegistrar
    {
        #region Methods
        /// <summary>
        /// Registers any routes to be ignored by the routing system.
        /// </summary>
        /// <param name="routes">The collection of routes to add to.</param>
        public void RegisterIgnoreRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ico/{*pathInfo}");
        }

        /// <summary>
        /// Registers any routes to be used by the routing system.
        /// </summary>
        /// <param name="routes">The collection of routes to add to.</param>
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Default", "{controller}/{action}", new { controller = "Home", action = "Index" });
        }
        #endregion
    }
}