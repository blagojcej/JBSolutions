using System;
using System.ComponentModel.Composition;

namespace JBSolutions.Common.Web.Contracts
{
    /// <summary>
    /// Marks the target class as an exportable registrar.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class), MetadataAttribute]
    public class ExportRegistrarAttribute : ExportAttribute, IOrderedMetadata
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="ExportRegistrarAttribute"/>.
        /// </summary>
        /// <param name="order">The order for this registrar.</param>
        public ExportRegistrarAttribute(int order = 100)
            : base(typeof(IRouteRegistrar))
        {
            Order = order;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the order.
        /// </summary>
        public int Order { get; private set; }
        #endregion
    }
}
