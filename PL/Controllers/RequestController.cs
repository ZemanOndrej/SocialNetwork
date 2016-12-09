using System.Web.Mvc;
using BL.Facades;
using Microsoft.AspNet.Identity;
using PL.Models;

namespace PL.Controllers
{
    public class RequestController : Controller
    {
		#region Dependency
		private readonly RequestFacade requestFacade;
	    private readonly UserFacade userFacade;

	    public RequestController(RequestFacade requestFacade, UserFacade userFacade)
	    {
		    this.requestFacade = requestFacade;
		    this.userFacade = userFacade;
	    }
		#endregion

		public ActionResult Index()
	    {

		    var user = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));
            return View(new RequestModel {Requests = requestFacade.ListRequestsForUserReceiver(user)});
        }

	    public ActionResult Accept(int id)
	    {
			requestFacade.AcceptRequest(id,int.Parse(User.Identity.GetUserId()));
		    return RedirectToAction("Index");
	    }

	    public ActionResult Delete(int id)
	    {
			requestFacade.DeleteRequest(id);
		    return RedirectToAction("Index");

	    }
		[HttpPost]
	    public ActionResult SendRequest(int id)
	    {
			requestFacade.SendRequest(int.Parse(User.Identity.GetUserId()), id);

			return RedirectToAction("UserPage", "Page",new {userId=id});
	    }

		[HttpPost]
	    public ActionResult RemoveFromFriends(int id)
	    {
			userFacade.RemoveUsersFromFriends(int.Parse(User.Identity.GetUserId()),id);
			return RedirectToAction("UserPage", "Page", new { userId = id });
		}
    }
}