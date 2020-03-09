using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Controllers
{
    [Authorize]
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        // GET: Error
        public ActionResult Error400()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Error404()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Error403()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Error500()
        {
            return View();
        }
    }
}