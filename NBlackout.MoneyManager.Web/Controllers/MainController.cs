using System.Web.Mvc;

namespace NBlackout.MoneyManager.Web.Controllers
{
    public class MainController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Consultation" });
        }
    }
}