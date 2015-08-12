using System.Web.Routing;

namespace JBSolutions.Common.Web.Contracts.Routing
{
    /// <summary>
    /// Defines the required contract for implementing a route registrar.
    /// </summary>
    public interface IRouteRegistrar
    {
        #region Methods
        /// <summary>
        /// Registers any routes to be ignored by the routing system.
        /// </summary>
        /// <param name="routes">The collection of routes to add to.</param>
        void RegisterIgnoreRoutes(RouteCollection routes);

        /// <summary>
        /// Registers any routes to be used by the routing system.
        /// </summary>
        /// <param name="routes">The collection of routes to add to.</param>
        void RegisterRoutes(RouteCollection routes);
        #endregion
    }
}