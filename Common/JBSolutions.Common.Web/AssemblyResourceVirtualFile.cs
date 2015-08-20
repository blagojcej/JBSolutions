using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace JBSolutions.Common.Web
{
    /// <summary>
    /// This class provides access to resources embedded in the plugin assemblies.
    /// </summary>
    public class AssemblyResourceVirtualFile : VirtualFile
    {
        #region Fields
        private string path;
        #endregion Fields

        #region Constructor
        
        /// <summary>
        /// Set the path variable during the instance call.
        /// </summary>
        /// <param name="virtualPath"></param>
        public AssemblyResourceVirtualFile(string virtualPath)
            : base(virtualPath)
        {
            path = VirtualPathUtility.ToAppRelative(virtualPath);
        }

        #endregion Constructor

        #region Methods
        
        /// <summary>
        ///  Override of the Open method of the System.Web.Hosting.VirtualPathProvider. 
        /// </summary>
        /// <returns>Stream to the resources embedded within the assembly as specified in the request path.</returns>
        public override Stream Open()
        {
            string[] parts = path.Split('/');
            //string assemblyName = parts[2];
            //string resourceName = parts[3];
            string assemblyName = string.Format(@"{0}\{1}\{2}\{3}",
                                    new object[] { parts[1], parts[2], parts[3], parts[4] });
            string resourceName = parts[5];

            //assemblyName = Path.Combine(HttpRuntime.BinDirectory, assemblyName);
            assemblyName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName);
            byte[] assemblyBytes = File.ReadAllBytes(assemblyName);
            Assembly assembly = Assembly.Load(assemblyBytes);
            string assemblyNamespace = assembly.GetName().Name;

            if (assembly != null)
            {
                //Check to see if this exists with the namespace. If so
                //then prepend the namespace.
                if (assembly.GetManifestResourceInfo(resourceName) == null)
                {
                    resourceName = assemblyNamespace + "." + resourceName;
                }
                return assembly.GetManifestResourceStream(resourceName);
            }

            return null;
        }

        #endregion Methods
    }
}
