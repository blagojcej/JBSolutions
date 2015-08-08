namespace JBSolutions.Common.Web.Contracts
{
    /// <summary>
    /// Defines the required contract for implementing named dependency metadata.
    /// </summary>
    public interface INamedDependencyMetadata : INamedMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the dependencies.
        /// </summary>
        string[] Dependencies { get; }
        #endregion
    }
}