using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using JBSolutions.Common.Web.Contracts;

namespace JBSolutions.Common.Web
{
    /// <summary>
    /// Creates instances of <see cref="IController"/> from exported parts.
    /// </summary>
    [Export(typeof(IControllerFactory))]
    public class MEFControllerFactory : DefaultControllerFactory
    {
        #region Fields
        private readonly CompositionContainer container;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="MEFControllerFactory"/>
        /// </summary>
        /// <param name="container">The current controller.</param>
        [ImportingConstructor]
        public MEFControllerFactory(CompositionContainer container)
        {
            Throw.IfArgumentNull(container, "container");

            this.container = container;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the specified controller by using the specified request context.
        /// </summary>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>A reference to the controller.</returns>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="requestContext"/> parameter is null.</exception>
        /// <exception cref="T:System.ArgumentException">The <paramref name="controllerName"/> parameter is null or empty.</exception>
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            var controller = container
                .GetExports<IController, INamedMetadata>()
                .Where(c => c.Metadata.Name.Equals(controllerName, StringComparison.OrdinalIgnoreCase))
                .Select(c => c.Value)
                .FirstOrDefault();

            return controller ?? base.CreateController(requestContext, controllerName);
        }
        #endregion
    }
}
