using Microsoft.Practices.Unity;

namespace JBSolutions.Common.Extensions
{
    /// <summary>
    /// Provides a Unity extension that registers types into the composition system.
    /// </summary>
    public class CSLExportProviderExtension : UnityContainerExtension
    {
        #region Fields
        private CSLExportProvider exportProvider;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="CSLExportProviderExtension" />
        /// </summary>
        /// <param name="exportProvider">The export provider.</param>
        public CSLExportProviderExtension(CSLExportProvider exportProvider)
        {
            Throw.IfArgumentNull(exportProvider, "exportProvider");

            this.exportProvider = exportProvider;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialises the extension.
        /// </summary>
        protected override void Initialize()
        {
            this.Context.Registering += (sender, e)
                                        => exportProvider.RegisterType(e.TypeFrom);

            this.Context.RegisteringInstance += (sender, e)
                                                => exportProvider.RegisterType(e.RegisteredType);
        }
        #endregion
    }
}
