using System;
using System.Web.Mvc;
using BL.DTO.GroupDTOs;
using BL.Facades;
using Microsoft.AspNet.Identity;
using PL.Models;
using Utils.Enums;

namespace PL.Controllers
{
	[Authorize]
	public class GroupController : Controller
	{
		#region Dependency
		private readonly GroupFacade groupFacade;
		private readonly PostFacade postFacade;
		private readonly UserFacade userFacade;

		public GroupController(GroupFacade groupFacade, PostFacade postFacade, UserFacade userFacade)
		{
			this.groupFacade = groupFacade;
			this.postFacade = postFacade;
			this.userFacade = userFacade;
		}
		#endregion

		public ActionResult Create()
		{
			return View(new GroupDTO());
		}

		[HttpPost]
		public ActionResult Create(GroupDTO group)
		{
			var grpId =groupFacade.CreateNewGroup(group,int.Parse(User.Identity.GetUserId()));
			return RedirectToAction("GroupPage","Page",new {groupId=grpId});
		}

		public ActionResult Edit(int id)
		{
			if (CheckIfUserIsInGroup(id))
			{
				return RedirectToAction("AccessDenied", "Page");
			}
			return View(groupFacade.GetGroupById(id));
		}

		[HttpPost]
		public ActionResult Edit(GroupDTO group)
		{
			groupFacade.EditGroup(group);
			return RedirectToAction("GroupPage","Page",new{groupId=group.ID});
		}

		public ActionResult Delete(int id)
		{
			if (CheckIfUserIsInGroup(id))
			{
				return RedirectToAction("AccessDenied", "Page");
			}
			return View();
		}

		[HttpPost]
		public ActionResult LeaveGroup(DefaultPageModel model)
		{
			groupFacade.RemoveUserFromGroup(model.Group.ID, int.Parse(User.Identity.GetUserId()));
			return RedirectToAction("FrontPage", "Page");
		}

		private bool CheckIfUserIsInGroup(int id)
		{
			return !groupFacade.ListUsersInGroup(groupFacade.GetGroupById(id))
				.Contains(userFacade.GetUserById(int.Parse(User.Identity.GetUserId())));

		}

	}
}
