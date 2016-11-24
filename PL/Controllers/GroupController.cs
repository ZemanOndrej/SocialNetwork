using System.Web.Mvc;
using BL.Facades;
using PL.Models;
using Utils.Enums;

namespace PL.Controllers
{
	public class GroupController : Controller
	{
		//TODO CHANGE GROUPID IN PostDto to GROUPDTO

		public GroupFacade GroupFacade { get; set; }
		public PostFacade PostFacade { get; set; }
		public UserFacade UserFacade { get; set; }


		public ActionResult GroupPage(int id)
		{

			var group = GroupFacade.GetGroupById(id);
			var posts = PostFacade.GetPostsFromGroup(group);

			return View(new GroupPageModel {Group = group,Posts = posts});
		}

		[HttpPost]
		public ActionResult GroupPage(GroupPageModel model)
		{

			var group = GroupFacade.GetGroupById(model.Group.ID);

			var user = UserFacade.GetUserByEmail(User.Identity.Name);
			model.NewPost.PrivacyLevel=PostPrivacyLevel.Group;
			if (user == null) user = UserFacade.GetUserById(1);
			PostFacade.SendPost(model.NewPost, user, group);


			return RedirectToAction("GroupPage",new {id=model.Group.ID});
		}




		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Group/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Group/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection)
		{
			try
			{
				// TODO: Add insert logic here

				return RedirectToAction("GroupPage");
			}
			catch
			{
				return View();
			}
		}

		// GET: Group/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Group/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Group/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

	   
	}
}
