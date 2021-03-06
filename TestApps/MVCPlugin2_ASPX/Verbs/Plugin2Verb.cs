﻿using System.ComponentModel.Composition;
using JBSolutions.Common.Web.Contracts.Web;

namespace MVCPlugin2_ASPX.Verbs
{
    /// <summary>
    /// Provides a navigational verb.
    /// </summary>
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "Navigation")]
    public class Plugin2Verb : IActionVerb
    {
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return "Plugin 2"; }
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
            get { return "MVCPlugin2"; }
        }
        #endregion
    }
}