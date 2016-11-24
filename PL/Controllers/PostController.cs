using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.DTO.GroupDTOs;
using BL.Facades;
using PL.Models;

namespace PL.Controllers
{
	public class PostController : Controller
	{
		public PostFacade PostFacade { get; set; }
		public UserFacade UserFacade { get; set; }
		public GroupFacade GroupFacade { get; set; }



		public ActionResult Edit(int id)
		{
			var post = PostFacade.GetPostById(id);

			return View(post);
		}

		[HttpPost]
		public ActionResult Edit(PostDTO post)
		{
			PostFacade.UpdatePostMessage(post);
			return RedirectToAction("FrontPage", "User");
		}

		public ActionResult Delete(int id)
		{
			var post = PostFacade.GetPostById(id);
			return View(post);
		}
			[HttpPost]
		public ActionResult Delete(PostDTO post)
			{
				PostFacade.DeletePost(post);
				return RedirectToAction("FrontPage", "User");

			}


		public ActionResult Reactions(int id)
		{


			var post = PostFacade.GetPostById(id);
			//todo treba spravit aby aj commenty sa dali commentovat
			var comments = PostFacade.GetCommentsOnPost(post);
			var reactions = PostFacade.GetReactionsOnPost(post);
			return View(new OpenPostModel { Post = post, Comments = comments, Reactions = reactions,PostId = post.ID});
		}

		[HttpPost]
		public ActionResult CreateComment(OpenPostModel model)
		{
			//logged in user
			var user = UserFacade.GetUserByEmail(User.Identity.Name);
			if (user == null) user = UserFacade.GetUserById(1);

			if (model.Post == null) model.Post = PostFacade.GetPostById(model.PostId);

			PostFacade.CommentPost(model.Post, model.NewComment, user);
			return RedirectToAction("Reactions", new {id = model.PostId});
		}

		[HttpPost]
		public ActionResult CreateReaction(OpenPostModel model)
		{
			return RedirectToAction("Reactions", new { id = model.PostId });

		}
	}
}
