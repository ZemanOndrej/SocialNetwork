using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BL.DTO.PostDTOs;
using BL.DTO.UserDTOs;
using BL.Facades;
using Microsoft.AspNet.Identity;
using PL.Models;
using Utils.Enums;

namespace PL.Controllers
{
	[Authorize]
	public class PageController : Controller
	{
		public ActionResult UserPage(int userId, int page = 1)
		{
			var currUser = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));

			if (currUser.ID == userId)
				return RedirectToAction("ProfilePage");


			var user = userFacade.GetUserById(userId);
			var model = new DefaultPageModel
			{
				Account = user,
				Page = page,
				InRelationship = userFacade.AreUsersFriends(currUser.ID, user.ID),
				PendingFriendRequest = userFacade.UserHasPendingFriendRequestFromUser(user, currUser),
				Accounts = new List<AccountDTO>(),
				Posts = postFacade.GetPostsFromUserForUser(user, currUser)
			};


			switch (user.FriendListVisibility)
			{
				case FriendListVisibilityLevel.OnlyFriends:
					if (model.InRelationship)
						model.Accounts = userFacade.ListFriendsOf(user);
					break;
				case FriendListVisibilityLevel.Public:
					model.Accounts = userFacade.ListFriendsOf(user);
					break;
			}


			return View(model);
		}


		public ActionResult ProfilePage(int page = 1)
		{
			var id = int.Parse(User.Identity.GetUserId());
			var account = userFacade.GetUserById(id);
			var friends = userFacade.ListFriendsOf(account);

			return View(new DefaultPageModel
			{
				Posts = postFacade.GetPostsFromUser(account, page),
				Account = account,
				Accounts = friends,
				Groups = userFacade.ListGroupsWithUser(account),
				Page = page,
				NewPost = new PostDTO {PrivacyLevel = account.DefaultPostPrivacy}
			});
		}

		public ActionResult FrontPage(int page = 1)
		{
			var user = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));
			return View(new DefaultPageModel
			{
				Posts = postFacade.GetPostsForUserFrontPage(user, page),
				NewPost = new PostDTO {PrivacyLevel = user.DefaultPostPrivacy},
				Groups = userFacade.ListGroupsWithUser(user),
				Page = page
			});
		}


		public ActionResult GroupPage(int groupId, int page = 1)
		{
			var group = groupFacade.GetGroupById(groupId);
			var model = new DefaultPageModel {Group = group, Page = page};
			var accounts = groupFacade.ListUsersInGroup(group);

			if (accounts.Select(u => u.ID).Contains(int.Parse(User.Identity.GetUserId())))
				model.InRelationship = true;
			if (model.InRelationship || (group.GroupPrivacyLevel == GroupPrivacyLevel.Public))
			{
				model.Accounts = accounts;
				model.Posts = postFacade.GetPostsFromGroup(group, page);
			}

			return View(model);
		}


		public ActionResult AccessDenied()
		{
			return View();
		}

		#region Dependency

		private readonly UserFacade userFacade;
		private readonly PostFacade postFacade;
		private readonly RequestFacade requestFacade;
		private readonly GroupFacade groupFacade;

		public PageController(UserFacade userFacade, PostFacade postFacade, RequestFacade requestFacade,
			GroupFacade groupFacade)
		{
			this.userFacade = userFacade;
			this.postFacade = postFacade;
			this.requestFacade = requestFacade;
			this.groupFacade = groupFacade;
		}

		#endregion
	}
}