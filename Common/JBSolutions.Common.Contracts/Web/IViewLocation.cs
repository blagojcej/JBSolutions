namespace JBSolutions.Common.Contracts.Web
{
    /// <summary>
    /// Defines View Location for each plugin
    /// </summary>
    public interface IViewLocation
    {
        #region Properties
        /// <summary>
        /// Path to views
        /// </summary>
        string ViewsPath { get; }

        #endregion Properties
    }
}