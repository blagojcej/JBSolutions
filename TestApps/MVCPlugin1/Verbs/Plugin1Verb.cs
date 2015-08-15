
using System.ComponentModel.Composition;
using JBSolutions.Common.Web.Contracts.Web;

namespace MVCPlugin1.Verbs
{
    /// <summary>
    /// Provides a navigational blog verb.
    /// </summary>
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "Navigation")]
    public class Plugin1Verb : IActionVerb
    {
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return "Plugin 1"; }
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        public string Action
        {
            get { return "Index"; }
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        public string Controller
        {
            get { return "Home"; }
        }
        #endregion
    }
}