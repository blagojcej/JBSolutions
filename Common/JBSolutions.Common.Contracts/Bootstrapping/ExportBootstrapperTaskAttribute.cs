﻿using System;
using System.ComponentModel.Composition;

namespace JBSolutions.Common.Web.Contracts.Bootstrapping
{
    /// <summary>
    /// Marks the target class as an exportable bootstrapper task.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class), MetadataAttribute]
    public class ExportBootstrapperTaskAttribute : ExportAttribute, INamedDependencyMetadata
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="ExportBootstrapperTaskAttribute"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="dependencies">Any named dependencies this task is explicitly dependent on.</param>
        public ExportBootstrapperTaskAttribute(string name, params string[] dependencies)
            : base(typeof(IBootstrapperTask))
        {
            //Throw.IfArgumentNullOrEmpty(name, "name");
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new Exception("Parameter Name can not be null or empty!");

            Dependencies = dependencies;
            Name = name;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the dependencies.
        /// </summary>
        public string[] Dependencies { get; private set; }

        /// <summary>
        /// Gets the name of the task.
        /// </summary>
        public string Name { get; private set; }
        #endregion
    }
}
