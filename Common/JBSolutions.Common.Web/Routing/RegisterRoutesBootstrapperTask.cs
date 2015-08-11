using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Routing;
using JBSolutions.Common.Web.Contracts;
using JBSolutions.Common.Web.Contracts.Bootstrapping;
using JBSolutions.Common.Web.Contracts.Routing;

namespace JBSolutions.Common.Web
{
    /// <summary>
    /// Registers any required routes with the routing system.
    /// </summary>
    [ExportBootstrapperTask("RegisterRoutes")]
    public class RegisterRoutesBootstrapperTask : IBootstrapperTask
    {
        #region Methods
        /// <summary>
        /// Runs the task.
        /// </summary>
        /// <param name="container"></param>
        public void Run(CompositionContainer container)
        {
            Throw.IfArgumentNull(container, "container");

            var registrars = container
                .GetExports<IRouteRegistrar, IOrderedMetadata>()
                .OrderBy(r => r.Metadata.Order)
                .Select(r => r.Value);

            var routes = RouteTable.Routes;

            foreach (var registrar in registrars)
                registrar.RegisterRoutes(routes);

        }
        #endregion
    }
}
