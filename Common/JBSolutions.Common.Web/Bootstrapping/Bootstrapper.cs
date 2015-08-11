using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Threading;
using System.Web.Mvc;
using JBSolutions.Common.Collections;
using JBSolutions.Common.Web.Contracts;
using JBSolutions.Common.Web.Contracts.Bootstrapping;

namespace JBSolutions.Common.Web
{
    /// <summary>
    /// Performs bootstrapping operations.
    /// </summary>
    public static class Bootstrapper
    {
        #region Fields
        private static bool initialised;
        private static readonly Mutex mutex = new Mutex();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current container.
        /// </summary>
        public static CompositionContainer Container { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Runs the bootstrapper.
        /// </summary>
        public static void Run()
        {
            if (Container == null)
                return;

            // Set the IDependencyResolver MVC uses to resolve types.
            DependencyResolver.SetResolver(new MEFDependencyResolver(Container));

            // Run any bootstrapper tasks.
            RunTasks();
        }

        /// <summary>
        /// Runs any required bootstrapper tasks.
        /// </summary>
        private static void RunTasks()
        {
            var tasks = Container.GetExports<IBootstrapperTask, INamedDependencyMetadata>();
            var list = new DependencyList<Lazy<IBootstrapperTask, INamedDependencyMetadata>, string>(
                l => l.Metadata.Name,
                l => l.Metadata.Dependencies);

            foreach (var task in tasks)
                list.Add(task);

            foreach (var task in list)
                task.Value.Run(Container);
        }

        /// <summary>
        /// Sets the factory used to create a container.
        /// </summary>
        /// <param name="factory">The container factory.</param>
        public static void SetContainerFactory(ICompositionContainerFactory factory)
        {
            Throw.IfArgumentNull(factory, "factory");

            mutex.WaitOne();

            if (initialised)
                return;

            try
            {
                var container = factory.CreateContainer();
                Throw.If(container == null).As<InvalidOperationException>(() => StringResources.FactoryDidntCreateContainer);

                Container = container;

                // Add the container to itself so it can be resolved.
                var batch = new CompositionBatch();
                batch.AddExportedValue(container);
                container.Compose(batch);

                initialised = true;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
        #endregion
    }
}
