using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace JBSolutions.Common.Web
{
    /// <summary>
    /// This class is used to hook into the MVC architecture. It provides overrides to intercept calls in order to pull
    /// resources requested by plugins directly from their assemblies as opposed to the filesystem.
    /// </summary>
    public class AssemblyResourceProvider : VirtualPathProvider
    {
        #region Helper Methods
        
        /// <summary>
        /// Check to see if requested resource is for a plugin.
        /// </summary>
        /// <param name="virtualPath">Path of the requested resource.</param>
        /// <returns>True if the requested reseource belongs to a plugin, otherwise it returns false.</returns>
        private bool IsAppResourcePath(string virtualPath)
        {
            String checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
            return checkPath.StartsWith("~/Plugins/", StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion Helper Methods

        #region Methods
        
        /// <summary>
        /// Override of the FileExists method of the System.Web.Hosting.VirtualPathProvider.
        /// This will check to see if the resource requested is for a plugin, and if so will
        /// check the plugin assembly to see if the resource exists. Otherwise, it will use the
        /// base FileExists method.
        /// </summary>
        /// <param name="virtualPath">Path of the requested resource.</param>
        /// <returns>True if the resource can be located. Otherwise it returns False.</returns>
        public override bool FileExists(string virtualPath)
        {
            if (IsAppResourcePath(virtualPath))
            {
                string path = VirtualPathUtility.ToAppRelative(virtualPath);
                string[] parts = path.Split('/');
                //string assemblyName = parts[2];
                //string resourceName = parts[3];
                string assemblyName = string.Format(@"{0}\{1}\{2}\{3}",
                                                    new object[] {parts[1], parts[2], parts[3], parts[4]});
                string resourceName = parts[5];

                //assemblyName = Path.Combine(HttpRuntime.BinDirectory, assemblyName);
                assemblyName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName);
                byte[] assemblyBytes = File.ReadAllBytes(assemblyName);
                Assembly assembly = Assembly.Load(assemblyBytes);
                string assemblyNamespace = assembly.GetName().Name;

                if (assembly != null)
                {
                    string[] resourceList = assembly.GetManifestResourceNames();
                    bool found = Array.Exists(resourceList, delegate(string r) { return r.Equals(resourceName); })
                        || Array.Exists(resourceList, delegate(string r) { return r.Equals(assemblyNamespace + "." + resourceName); });

                    return found;
                }
                return false;
            }
            else
                return base.FileExists(virtualPath);
        }

        /// <summary>
        /// Override of the GetFile method of the System.Web.Hosting.VirtualPathProvider.
        /// This will check to see if the resource requested is for a plugin, and if so will
        /// return the resource that is embedded within the plugin assembly. Otherwise, it will use the
        /// base GetFile method.
        /// </summary>
        /// <param name="virtualPath">Path of the requested resource.</param>
        /// <returns>VirtualFile object of the requested resource.</returns>
        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsAppResourcePath(virtualPath))
                return new AssemblyResourceVirtualFile(virtualPath);
            else
                return base.GetFile(virtualPath);
        }

        /// <summary>
        /// Override of the GetCacheDependency method of the System.Web.Hosting.VirtualPathProvider. 
        /// This will check to see if the resource requested is for a plugin, and if so will return null.
        /// Otherwise it will use the base GetCacheDependency method.
        /// </summary>
        /// <param name="virtualPath">Path of the requested resource.</param>
        /// <param name="virtualPathDependencies">An array of cache keys (depemdant virtual paths) that the new object monitors for changes.</param>
        /// <param name="utcStart">The Time against which to check the last modified date of the objects in the arrays and the System.Web.Caching.CacheDependency object.</param>
        /// <returns>CacheDependency object relating the requested virtual path to its dependant virtual paths.</returns>
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (IsAppResourcePath(virtualPath))
            {
                return null;
            }
            return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        #endregion Methods
    }
}
