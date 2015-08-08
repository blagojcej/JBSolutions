namespace JBSolutions.Common.Web.Contracts
{
    /// <summary>
    /// Defines the required contract for implementing named metadata.
    /// </summary>
    public interface INamedMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
        #endregion
    }
}