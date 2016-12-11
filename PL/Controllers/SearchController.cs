using System.Web.Mvc;
using BL.Facades;
using PL.Models;

namespace PL.Controllers
{
	[Authorize]
    public class SearchController : Controller
    {
		#region Dependency
		private readonly GroupFacade groupFacade;
	    private readonly UserFacade userFacade;

	    public SearchController(UserFacade userFacade, GroupFacade groupFacade)
	    {
		    this.userFacade = userFacade;
		    this.groupFacade = groupFacade;
	    }
		#endregion

		

		[HttpPost]
	    public ActionResult Index(FormCollection fc, string searchString)
	    {
		    var groups = groupFacade.ListGroupsWithName(searchString);
		    var accounts = userFacade.GetUsersWithName(searchString);
			
			return View("Results",new SearchModel { FoundAccounts = accounts, FoundGroups = groups, Keyword =searchString});
		}

	    
    }
}