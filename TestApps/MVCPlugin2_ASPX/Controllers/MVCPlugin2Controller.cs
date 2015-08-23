using System.Web.Mvc;
using JBSolutions.Common.Contracts.Web;

namespace MVCPlugin2_ASPX.Controllers
{
    [ExportController("MVCPlugin2")]
    public class MVCPlugin2Controller : Controller
    {
        //
        // GET: /MVCPlugin2/

        public ActionResult Index()
        {
            return View();
        }

    }
}
