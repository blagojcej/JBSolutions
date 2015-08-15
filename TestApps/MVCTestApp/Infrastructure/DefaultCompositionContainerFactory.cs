using System.ComponentModel.Composition.Hosting;
using System.Web.Hosting;
using JBSolutions.Common.Web.Contracts;

namespace MVCTestApp.Infrastructure
{
    /// <summary>
    /// Creates a composition container.
    /// </summary>
    public class DefaultCompositionContainerFactory : ICompositionContainerFactory
    {
        #region Methods
        /// <summary>
        /// Creates a <see cref="CompositionContainer"/>.
        /// </summary>
        /// <returns>A <see cref="CompositionContainer"/>.</returns>
        public CompositionContainer CreateContainer()
        {
            string path = HostingEnvironment.MapPath("~/bin");
            var catalog = new DirectoryCatalog(path);

            return new CompositionContainer(catalog);
        }
        #endregion

    }
}