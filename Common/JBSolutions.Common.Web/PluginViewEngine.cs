using System;
using System.Web;
using System.Web.Mvc;

namespace JBSolutions.Common.Web
{
    public class PluginViewEngine : System.Web.Mvc.WebFormViewEngine
    {
        public PluginViewEngine(string[] viewLocations)
            : base()
        {
            string[] tempArray = new string[ViewLocationFormats.Length + viewLocations.Length];
            ViewLocationFormats.CopyTo(tempArray, 0);

            for (int i = 0; i < viewLocations.Length; i++)
            {
                tempArray[ViewLocationFormats.Length + i] = viewLocations[i];
            }

            ViewLocationFormats = tempArray;

            PartialViewLocationFormats = ViewLocationFormats;
        }

        private bool IsAppResourcePath(string virtualPath)
        {
            String checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
            return checkPath.StartsWith("~/Plugins/", StringComparison.InvariantCultureIgnoreCase);
        }

        //If we have a virtual path, we need to override the super class behavior,
        //its implementation ignores custom VirtualPathProviders, unlike the super's super class. 
        //This code basically just reimplements the super-super class (VirtualPathProviderViewEngine) behavior for virtual paths.
        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            if (IsAppResourcePath(virtualPath))
            {
                return System.Web.Hosting.HostingEnvironment.VirtualPathProvider.FileExists(virtualPath);
            }
            else
                return base.FileExists(controllerContext, virtualPath);
        }
    }
}
