using System.Linq;
using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Microsoft.AspNet.Identity;
using PL.Models;
using Utils.Enums;

namespace PL.Controllers
{
	[Authorize]
	public class PageController : Controller
	{
		#region Dependency
		private readonly UserFacade userFacade;
		private readonly PostFacade postFacade;
		private readonly RequestFacade requestFacade;
		private readonly GroupFacade groupFacade;

		public PageController(UserFacade userFacade, PostFacade postFacade, RequestFacade requestFacade, GroupFacade groupFacade)
		{
			this.userFacade = userFacade;
			this.postFacade = postFacade;
			this.requestFacade = requestFacade;
			this.groupFacade = groupFacade;
		}
		#endregion

		public ActionResult UserPage(int userId,int page=1)
		{
			

			if (  int.Parse(User.Identity.GetUserId()) == userId)
			{
				return RedirectToAction("ProfilePage");
			}


			var user = userFacade.GetUserById(userId);
			var friends = userFacade.ListFriendsOf(user);
			var model = new DefaultPageModel
			{
				Posts = postFacade.GetPostsFromUser(user,page), Account = user, Accounts = friends,Page = page
			};

			if (friends.Select(u => u.ID).Contains(int.Parse(User.Identity.GetUserId())))
			{
				model.InRelationship = true;
			}
			var reqsForAcc = requestFacade.ListRequestsForUserReceiver(userFacade.GetUserById(userId));
			if (reqsForAcc
				.Select(u => u.Sender.ID)
				.Contains(int.Parse(User.Identity.GetUserId())))
			{
				model.PendingFriendRequest = true;
			}


			return View(model);
		}


		public ActionResult ProfilePage(int page=1)
		{
			var id = int.Parse(User.Identity.GetUserId());
			var account = userFacade.GetUserById(id);
			var friends = userFacade.ListFriendsOf(account);

			return View(new DefaultPageModel
			{
				Posts = postFacade.GetPostsFromUser(account,page), Account = account ,Accounts = friends,
				Page = page,
				NewPost = new PostDTO { PrivacyLevel = account.DefaultPostPrivacy }
			});

		}

		public ActionResult FrontPage(int page=1)
		{
			var user = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));
			return View(new DefaultPageModel
			{
				
				Posts = postFacade.GetPostsForUserFrontPage(user,page),
				NewPost = new PostDTO { PrivacyLevel = user.DefaultPostPrivacy },
				Groups = userFacade.ListGroupsWithUser(user),
				Page = page
				
			});
		}


		


		public ActionResult GroupPage(int groupId , int page=1)
		{

			var group = groupFacade.GetGroupById(groupId);
			var posts = postFacade.GetPostsFromGroup(group,page);
			var accounts = groupFacade.ListUsersInGroup(group);
			var model = new DefaultPageModel {Group = group, Posts = posts, Accounts = accounts, Page = page};
			if (accounts.Select(u => u.ID).Contains(int.Parse(User.Identity.GetUserId())))
			{
				model.InRelationship = true;
			}

			return View(model);
		}



		public ActionResult AccessDenied()
		{
			return View();
		}

	}
}
