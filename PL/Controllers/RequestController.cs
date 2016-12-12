using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BL.DTO.ChatDTOs;
using BL.DTO.GroupDTOs;
using BL.DTO.UserDTOs;
using BL.Facades;
using Microsoft.AspNet.Identity;
using PL.Models;

namespace PL.Controllers
{
	[Authorize]
	public class RequestController : Controller
	{
		public ActionResult Index()
		{
			var user = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));
			return View(new RequestModel {Requests = requestFacade.ListRequestsForUserReceiver(user)});
		}

		public ActionResult Accept(int id)
		{
			var user = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));

			if (requestFacade.GetRequestById(id).Receiver.ID != user.ID)
				return RedirectToAction("AccessDenied", "Page");
			requestFacade.AcceptRequest(id, user.ID);
			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			var user = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));

			if ((requestFacade.GetRequestById(id).Receiver.ID != user.ID) ||
				(requestFacade.GetRequestById(id).Sender.ID != user.ID))
				return RedirectToAction("AccessDenied", "Page");
			requestFacade.DeleteRequest(id);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult SendRequest(int id)
		{
			requestFacade.SendRequest(int.Parse(User.Identity.GetUserId()), id);

			return RedirectToAction("UserPage", "Page", new {userId = id});
		}

		[HttpPost]
		public ActionResult RemoveFromFriends(int id)
		{
			userFacade.RemoveUsersFromFriends(int.Parse(User.Identity.GetUserId()), id);
			return RedirectToAction("UserPage", "Page", new {userId = id});
		}

		public ActionResult Invite(int? chatId, int? groupId)
		{
			var user = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));

			if (groupId != null)
				if (userFacade.ListGroupsWithUser(user).All(g => g.ID != groupId))
					return RedirectToAction("AccessDenied", "Page");

			var list = AccHelper(null, groupId, chatId);

			return View(new InvitePeopleModel {Invites = list, ChatId = chatId, GroupId = groupId});
		}

		[HttpPost]
		public ActionResult Invite(InvitePeopleModel model)
		{
			var currUser = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));
			if (model.GroupId != null)
			{
				foreach (var user in model.Invites)
					if (user.Invited)
						requestFacade.SendRequest(currUser.ID, user.Account.ID, model.GroupId.Value);
				return RedirectToAction("GroupPage", "Page", new {groupId = model.GroupId});
			}

			if (model.ChatId != null)
			{
				var tmp = chatFacade.AddUsersToChat(chatFacade.GetChatById(model.ChatId.Value),
					model.Invites.Where(s => s.Invited).Select(s => s.Account).ToList());

				if (tmp > 0)
					model.ChatId = tmp;

				return RedirectToAction("OpenChat", "Chat", new {id = model.ChatId});
			}

			if (model.ChatId == null)
			{
				var list = model.Invites.Where(i => i.Invited).Select(i => i.Account).ToList();
				list.Add(currUser);
				var chatId = chatFacade.CreateGroupChat(list);
				return RedirectToAction("OpenChat", "Chat", new {id = chatId});
			}

			return null;
		}

		[HttpPost]
		public ActionResult Search(string searchString, int? groupId = null, int? chatId = null)
		{
			var list = AccHelper(userFacade.GetUsersWithName(searchString), groupId, chatId);

			return View("Invite", new InvitePeopleModel {Invites = list, GroupId = groupId, ChatId = chatId});
		}

		private List<SelectModel> AccHelper(ICollection<AccountDTO> searchResults = null, int? groupId = null,
			int? chatId = null)
		{
			var user = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));
			var accountList = userFacade.ListFriendsOf(user);
			if (searchResults != null)
			{
				searchResults.Remove(user);
				accountList = accountList.Concat(searchResults).ToList();
				accountList = accountList.Distinct().ToList();
			}
			if (groupId != null)
			{
				var groupUsers = groupFacade.ListUsersInGroup(new GroupDTO {ID = groupId.Value});
				accountList = accountList.Where(a => !groupUsers.Contains(a)).ToList();
			}
			if (chatId != null)
			{
				var chatUsers = chatFacade.ListAllUsersInChat(new ChatDTO {ID = chatId.Value});
				accountList = accountList.Where(a => !chatUsers.Contains(a)).ToList();
			}

			var list = new List<SelectModel>();
			accountList.ForEach(f => list.Add(new SelectModel {Account = f}));
			return list;
		}

		#region Dependency

		private readonly RequestFacade requestFacade;
		private readonly UserFacade userFacade;
		private readonly ChatFacade chatFacade;
		private readonly GroupFacade groupFacade;

		public RequestController(RequestFacade requestFacade, UserFacade userFacade, ChatFacade chatFacade,
			GroupFacade groupFacade)
		{
			this.requestFacade = requestFacade;
			this.userFacade = userFacade;
			this.chatFacade = chatFacade;
			this.groupFacade = groupFacade;
		}

		#endregion
	}
}