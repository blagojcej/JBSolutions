using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using JBSolutions.Common.Contracts.Web;

namespace MVCPlugin2_ASPX
{
    [Export(typeof(IViewLocation)), ExportMetadata("ViewLocationPluginName", "MVCPlugin2")]
    public class MVCPlugin1ViewLocation : IViewLocation
    {
        public string ViewsPath
        {
            get { return "~/Plugins/Temp/Plugins/MVCPlugin2_ASPX.dll/MVCPlugin2_ASPX.Views.{1}.{0}.cshtml"; }
        }
    }
}