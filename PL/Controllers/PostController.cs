using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Microsoft.AspNet.Identity;
using PL.Models;

namespace PL.Controllers
{
	[Authorize]
	public class PostController : Controller
	{
		public PostFacade PostFacade { get; set; }
		public UserFacade UserFacade { get; set; }
		public GroupFacade GroupFacade { get; set; }


		public ActionResult Edit(int id, string viewName)
		{
			var post = PostFacade.GetPostById(id);
			return View(new PostEditModel {Post = post,BackView = viewName});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(PostEditModel model)
		{
			PostFacade.UpdatePostMessage(model.Post);
			return ResolveBackView(model);
		}

		public ActionResult Delete(int id,string viewName)
		{
			var post = PostFacade.GetPostById(id);
			return View(new PostEditModel {Post = post,BackView = viewName});
		}

			[HttpPost]
		public ActionResult Delete(PostEditModel model)
			{
				PostFacade.DeletePost(model.Post.ID);
				return ResolveBackView(model);

			}


		public ActionResult Reactions(int id, string viewName)
		{


			var post = PostFacade.GetPostById(id);
			var comments = PostFacade.GetCommentsOnPost(post);
			var reactions = PostFacade.GetReactionsOnPost(post);
			return View(new OpenPostModel { Post = post, Comments = comments, Reactions = reactions,PostId = post.ID, BackView = viewName});
		}

		[HttpPost]
		public ActionResult CreateComment(OpenPostModel model)
		{
			//logged in account
			var user = UserFacade.GetUserById(int.Parse(User.Identity.GetUserId()));

			if (model.Post == null) model.Post = PostFacade.GetPostById(model.PostId);

			PostFacade.CommentPost(model.Post, model.NewComment, user);
			return RedirectToAction("Reactions", new {id = model.PostId,viewName=model.BackView });
		}

		[HttpPost]
		public ActionResult CreateReaction(OpenPostModel model)
		{
			return RedirectToAction("Reactions", new { id = model.PostId });

		}





		#region Utils

		public ActionResult ResolveBackView(PostEditModel model)
		{
			switch (model.BackView)
			{
				case "GroupPage":
					return RedirectToAction("GroupPage", "Page",new {groupId=model.Post.Group.ID});
				case "UserPage":
					return RedirectToAction("UserPage", "Page", new {userId = model.Post.Sender.ID });
				default:
					return RedirectToAction(model.BackView, "Page");
			}
		}

		#endregion
		
	}
}
