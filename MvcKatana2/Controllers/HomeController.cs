using System.Web.Mvc;

namespace MvcKatana2.Controllers
{
    [Route("{action=index}")]
    public class HomeController : Controller
    {
        [Authorize(Roles = "RegularUser")]
        public ActionResult Index()
        {
            ViewBag.Title = "We've Got Issues...";
            return View();
        }
	}
}
