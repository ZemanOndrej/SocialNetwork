using System.Web.Mvc;
using BL.Facades;
using Microsoft.AspNet.Identity;
using PL.Models;
using Utils.Enums;

namespace PL.Controllers
{
	[Authorize]
	public class GroupController : Controller
	{

		public GroupFacade GroupFacade { get; set; }
		public PostFacade PostFacade { get; set; }
		public UserFacade UserFacade { get; set; }


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
