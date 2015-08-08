namespace JBSolutions.Common.Web.Contracts
{
    /// <summary>
    /// Defines the required contract for implementing ordered metadata.
    /// </summary>
    public interface IOrderedMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the order.
        /// </summary>
        int Order { get; }
        #endregion
    }
}