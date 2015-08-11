using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Web.Http.Dependencies;

namespace JBSolutions.Common.Web
{
    /// <summary>
    /// Resolves types using the Managed Extensibility Framework.
    /// </summary>
    public class MEFDependencyResolver : IDependencyResolver
    {
        #region Fields
        private readonly CompositionContainer container;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="MEFDependencyResolver"/>.
        /// </summary>
        /// <param name="container">The current container.</param>
        public MEFDependencyResolver(CompositionContainer container)
        {
            Throw.IfArgumentNull(container, "container");

            this.container = container;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets an instance of the service of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>An instance of the service of the specified type.</returns>
        public object GetService(Type type)
        {
            Throw.IfArgumentNull(type, "type");

            string name = AttributedModelServices.GetContractName(type);

            try
            {
                return container.GetExportedValue<object>(name);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all instances of the services of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>An enumerable of all instances of the services of the specified type.</returns>
        public IEnumerable<object> GetServices(Type type)
        {
            Throw.IfArgumentNull(type, "type");

            string name = AttributedModelServices.GetContractName(type);

            try
            {
                return container.GetExportedValues<object>(name);
            }
            catch
            {
                return null;
            }
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            ;
        }
        #endregion
    }
}
