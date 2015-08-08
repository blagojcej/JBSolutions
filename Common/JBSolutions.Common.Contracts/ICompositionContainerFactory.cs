using System.ComponentModel.Composition.Hosting;

namespace JBSolutions.Common.Web.Contracts
{
    /// <summary>
    /// Defines the required contract for implementing a container factory.
    /// </summary>
    public interface ICompositionContainerFactory
    {
        #region Methods
        /// <summary>
        /// Creates a <see cref="CompositionContainer"/>.
        /// </summary>
        /// <returns>A <see cref="CompositionContainer"/>.</returns>
        CompositionContainer CreateContainer();
        #endregion
    }
}