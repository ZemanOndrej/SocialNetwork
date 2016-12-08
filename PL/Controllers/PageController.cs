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

		public ActionResult UserPage(int userId)
		{
			

			if (  int.Parse(User.Identity.GetUserId()) == userId)
			{
				return RedirectToAction("ProfilePage");
			}


			var user = userFacade.GetUserById(userId);
			var friends = userFacade.ListFriendsOf(user);
			var model = new DefaultPageModel
			{
				Posts = postFacade.GetPostsFromUser(user), Account = user, Accounts = friends
			};

			if (friends.Select(u => u.ID).Contains(int.Parse(User.Identity.GetUserId())))
			{
				model.IsYourFriend = true;
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

		[HttpPost]
		public ActionResult ProfilePage(DefaultPageModel model)
		{

			CreatePost(model.NewPost);
			return RedirectToAction("ProfilePage");
		}

		public ActionResult ProfilePage()
		{
			var id = int.Parse(User.Identity.GetUserId());
			var account = userFacade.GetUserById(id);
			var friends = userFacade.ListFriendsOf(account);

			return View(new DefaultPageModel
			{
				Posts = postFacade.GetPostsFromUser(account), Account = account ,Accounts = friends,
				NewPost = new PostDTO { PrivacyLevel = account.DefaultPostPrivacy }
			});

		}



		public ActionResult FrontPage()
		{
			var user = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));
			return View(new DefaultPageModel
			{
				Posts = postFacade.GetPostsForUserFrontPage(user),
				NewPost = new PostDTO { PrivacyLevel = user.DefaultPostPrivacy }
			});
		}

		[HttpPost]
		public ActionResult FrontPage(DefaultPageModel model)
		{
			CreatePost(model.NewPost);
			return RedirectToAction("FrontPage");
		}

		public void CreatePost(PostDTO post)
		{

			var account = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));
			postFacade.SendPost(post, account);
		}


		public ActionResult GroupPage(int groupId)
		{

			var group = groupFacade.GetGroupById(groupId);
			var posts = postFacade.GetPostsFromGroup(group);
			var accounts = groupFacade.ListUsersInGroup(group);

			return View(new DefaultPageModel { Group = group, Posts = posts,Accounts = accounts});
		}

		[HttpPost]
		public ActionResult GroupPage(DefaultPageModel model)
		{

			var group = groupFacade.GetGroupById(model.Group.ID);

			var user = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));
			model.NewPost.PrivacyLevel = PostPrivacyLevel.Group;

			postFacade.SendPost(model.NewPost, user, group);


			return RedirectToAction("GroupPage", new { groupId = model.Group.ID });
		}


	}
}
