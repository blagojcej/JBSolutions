using System.Web.Mvc;
using JBSolutions.Common.Contracts.Web;

namespace MVCPlugin1.Controllers
{
    [ExportController("MVCPlugin1")]
    public class MVCPlugin1Controller : Controller
    {
        //
        // GET: /MVCPlugin1/

        public ActionResult Index()
        {
            ViewResult RenderedView = View("~/Plugins/Plugins/Temp/Plugins/MVCPlugin1.dll/MVCPlugin1.Views.MVCPlugin1.Index.cshtml");
            return RenderedView;
        }

    }
}
