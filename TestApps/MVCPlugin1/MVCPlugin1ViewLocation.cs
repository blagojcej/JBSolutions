using System.ComponentModel.Composition;
using JBSolutions.Common.Contracts.Web;

namespace MVCPlugin1
{
    [Export(typeof(IViewLocation)), ExportMetadataAttribute("ViewLocationPluginName", "MVCPlugin1")]
    public class MVCPlugin1ViewLocation : IViewLocation
    {
        public string ViewsPath
        {
            get { return "~/Plugins/Temp/Plugins/MVCPlugin1.dll/MVCPlugin1.Views.{1}.{0}.cshtml"; }
        }
    }
}