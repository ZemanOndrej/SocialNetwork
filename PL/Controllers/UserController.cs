using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Microsoft.AspNet.Identity;
using PL.Models;

namespace PL.Controllers
{
	public class UserController : Controller
	{
		public UserFacade UserFacade { get; set; }
		public  PostFacade PostFacade { get; set; }

		public ActionResult UserPage(int id)
		{

			if(id==1)
			// nejsom prihlaseny
			//if (  int.TryParse(User.Identity.GetUserId()) == id)
			{
				return RedirectToAction("ProfilePage");
			}

				var user = UserFacade.GetUserById(id) ?? UserFacade.GetUserById(1);


				return View(new UserPageModel { Posts = PostFacade.GetPostsFromUser(user), User = user });
		}



//		public ActionResult UserPage()
//		{
//			var user = UserFacade.GetUserByEmail(User.Identity.Name);
//			if (user == null) user = UserFacade.GetUserById(1);
//
//			return View(new UserPageModel { Posts = PostFacade.GetPostsFromUser(user), User = user });
//		}


		[HttpPost]
		public ActionResult ProfilePage(UserPageModel model)
		{

			CreatePost(model.NewPost);
			return RedirectToAction("ProfilePage");
		}

		public ActionResult ProfilePage()
		{
			UserDTO user;
			if (User.Identity.GetUserId() == null)
			{
				user = UserFacade.GetUserById(1);

			}
			else
			{
				user = UserFacade.GetUserById(int.Parse(User.Identity.GetUserId()));

			}
			
			return View(new UserPageModel { Posts = PostFacade.GetPostsFromUser(user), User = user });

		}



		public ActionResult FrontPage()
		{
			return View(new FrontPageModel { Posts = PostFacade.ListAllPosts() });
		}

		[HttpPost]
		public ActionResult FrontPage(FrontPageModel model)
		{
			CreatePost(model.NewPost);
			return RedirectToAction("FrontPage");
		}




		public void CreatePost(PostDTO post)
		{
			UserDTO user;



			if (User.Identity.GetUserId() == null)
			{
				user = UserFacade.GetUserById(1);

			}
			else
			{
				user = UserFacade.GetUserById(int.Parse(User.Identity.GetUserId()));

			}
			if (user == null) user = UserFacade.GetUserById(1);


			PostFacade.SendPost(post, user);
		}

		





		public ActionResult Index(int id)
		{
			var user = UserFacade.GetUserById(id);
			return View(user);
		}

		

		// GET: User/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: User/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: User/Create
		[HttpPost]
		public ActionResult Create(FormCollection collection)
		{
			try
			{
				// TODO: Add insert logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: User/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: User/Edit/5
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

		// GET: User/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: User/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction("Index","Home");
			}
			catch
			{
				return View();
			}
		}


	}
}
