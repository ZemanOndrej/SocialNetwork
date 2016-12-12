using System.Web.Mvc;

namespace PL.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
				return RedirectToAction("FrontPage", "Page");
			return View();
		}
	}
}