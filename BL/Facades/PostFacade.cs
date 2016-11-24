using System.Collections.Generic;
using System.Linq;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.GroupDTOs;
using BL.Services.Comment;
using BL.Services.Post;
using BL.Services.Reaction;
using Utils.Enums;

namespace BL.Facades
{
	public class PostFacade
	{
		#region Dependency
		private readonly IPostService postService;
		private readonly IReactionService reactionService;
		private readonly ICommentService commentService;

		public PostFacade(IPostService postService, IReactionService reactionService, ICommentService commentService)
		{
			this.postService = postService;
			this.reactionService = reactionService;
			this.commentService = commentService;
		}
	
		#endregion

		public int SendPost( PostDTO post ,UserDTO user, GroupDTO group = null)
		{
			post.Group = group;
			post.Sender = user;
			
			return postService.CreatePost(post);
		}

		public void DeletePost(PostDTO post)
		{
			postService.DeletePost(post.ID);
		}

		public void UpdatePostMessage(PostDTO post)
		{
			postService.EditPostMessage(post);
		}

		public List<PostDTO> GetPostsFromGroup(GroupDTO group, int page = 0)
		{
			return postService.ListPosts(new PostFilter {Group = group}, page).ResultPosts.ToList();
		}

		public List<PostDTO> GetPostsFromUser(UserDTO user, int page = 0)
		{
			return postService.ListPosts(new PostFilter { Sender = user}, page).ResultPosts.ToList();

		}

		public PostDTO GetPostById(int id)
		{
			return postService.GetPostById(id);
		}

		public void UpdateComment(CommentDTO comment)
		{
			commentService.EditCommentMessage(comment);
		}

		public void UpdateReaction(ReactionDTO reaction)
		{
			reactionService.EditReaction(reaction);
		}

		public void DeleteReaction(ReactionDTO reaction)
		{
			reactionService.DeleteReaction(reaction.ID);
		}

		public void DeleteComment(CommentDTO comment)
		{
			commentService.DeleteComment(comment.ID);
		}

		public int CommentPost(PostDTO post, CommentDTO comment, UserDTO user)
		{
			comment.Sender = user;
			comment.Post = post;
			return commentService.CreateComment(comment);
		}

		public int ReactOnPost(PostDTO post, ReactionDTO reaction, UserDTO user)
		{

			return reactionService.CreateReaction(post,reaction,user);
		}

		public List<ReactionDTO> GetReactionsOnPost(PostDTO post)
		{
			return reactionService.ListReactions(new ReactionFilter {Post = post}).ResultReactions.ToList();
		}

		public ReactionDTO GetReactionById(int id)
		{
			return reactionService.GetReactionById(id);
		}

		public CommentDTO GetCommentById(int id)
		{
			return	commentService.GetCommentById(id);
		}

		public List<CommentDTO> GetCommentsOnPost(PostDTO post, int page = 0)
		{
			return commentService.ListComments(new CommentFilter {Post = post}, page).ResultComments.ToList();
		}



		public List<CommentDTO> ListAllComments()
		{
			return commentService.ListComments(new CommentFilter()).ResultComments.ToList();

		}
		public List<ReactionDTO> ListAllReactions()
		{
			return reactionService.ListReactions(new ReactionFilter()).ResultReactions.ToList();

		}
		public List<PostDTO> ListAllPosts()
		{
			return postService.ListPosts(new PostFilter()).ResultPosts.ToList();

		}


	}
}