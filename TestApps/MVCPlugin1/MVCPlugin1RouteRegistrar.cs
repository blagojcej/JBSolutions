using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Routing;
using JBSolutions.Common.Web.Contracts.Routing;

namespace MVCPlugin1
{
    /// <summary>
    /// Registers the default MVC routes.
    /// </summary>
    [Export(typeof(IRouteRegistrar)), ExportMetadata("Order", 105)]
    public class MVCPlugin1RouteRegistrar : IRouteRegistrar
    {
        #region Methods
        /// <summary>
        /// Registers any routes to be ignored by the routing system.
        /// </summary>
        /// <param name="routes">The collection of routes to add to.</param>
        public void RegisterIgnoreRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*staticfile}", new { staticfile = @".*\.(css|js|gif|jpg|png)(/.*)?" });
        }

        /// <summary>
        /// Registers any routes to be used by the routing system.
        /// </summary>
        /// <param name="routes">The collection of routes to add to.</param>
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("MVCPlugin1", "{controller}/{action}/{id}",
                            new { controller = "MVCPlugin1", action = "Index", id = UrlParameter.Optional },
                            new string[] {"MVCPlugin1.Controllers"});
        }
        #endregion

    }
}