using System.ComponentModel.Composition.Hosting;

namespace JBSolutions.Common.Web.Contracts.Bootstrapping
{
    /// <summary>
    /// Defines the required contract for implementing a bootstrapper task.
    /// </summary>
    public interface IBootstrapperTask
    {
        #region Methods
        /// <summary>
        /// Runs the task.
        /// </summary>
        /// <param name="container"></param>
        void Run(CompositionContainer container);
        #endregion
    }
}