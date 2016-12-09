using System.Collections.Generic;
using System.Linq;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.GroupDTOs;
using BL.DTO.UserDTOs;
using BL.Services.Comment;
using BL.Services.Group;
using BL.Services.Post;
using BL.Services.Reaction;
using BL.Services.User;
using Utils.Enums;

namespace BL.Facades
{
	public class PostFacade
	{
		#region Dependency
		private readonly IPostService postService;
		private readonly IReactionService reactionService;
		private readonly ICommentService commentService;
		private readonly IUserService userService;

		public PostFacade(IPostService postService, IReactionService reactionService, ICommentService commentService, IUserService userService)
		{
			this.postService = postService;
			this.reactionService = reactionService;
			this.commentService = commentService;
			this.userService = userService;
		}
	
		#endregion

		public int SendPost( PostDTO post ,AccountDTO account, GroupDTO group = null)
		{
			post.Group = group;
			post.Sender = account;
			
			return postService.CreatePost(post);
		}

		public void DeletePost(PostDTO post)
		{
			postService.DeletePost(post.ID);
		}

		public void DeletePost(int id)
		{
			postService.DeletePost(id);
		}

		public void UpdatePostMessage(PostDTO post)
		{
			postService.EditPostMessage(post);
		}

		public List<PostDTO> GetPostsFromGroup(GroupDTO group, int page = 0)
		{
			return postService.ListPosts(new PostFilter {Group = group}, page).ResultPosts.ToList();
		}

		public List<PostDTO> GetPostsFromUser(AccountDTO account, int page = 0)
		{
			return postService.ListPosts(new PostFilter { Sender = account}, page).ResultPosts.ToList();

		}

		public List<PostDTO> GetPostsForUserFrontPage(int accountId, int page = 0)
		{
			//TODO visible posts from friends and groups

			var account = userService.GetUserById(accountId);
			return GetPostsForUserFrontPage(account, page);

		}

		public List<PostDTO> GetPostsForUserFrontPage(AccountDTO account, int page = 0)
		{
			//TODO visible posts from friends and groups

			return postService.ListPosts(new PostFilter
			{
				FrontPageFilter = new FrontPageFilter
				{
					Account = account
					,Friends = userService.ListFriendsOfUser(account)
					,Groups = userService.ListUsersGroups(account)
				}
			}, page).ResultPosts.ToList();

		}

		public PostDTO GetPostById(int id)
		{
			return postService.GetPostById(id);
		}

		public void UpdateComment(CommentDTO comment)
		{
			commentService.EditCommentMessage(comment);
		}

		public ReactionDTO GetReactionOfUserOnPost(PostDTO post, AccountDTO account)
		{
			return reactionService.ListReactions(new ReactionFilter {Account = account, Post = post})
				.ResultReactions
				.FirstOrDefault();
		}

		public void UpdateReaction(ReactionDTO reaction)
		{
			reactionService.EditReaction(reaction);
		}

		public void DeleteReaction(ReactionDTO reaction)
		{
			reactionService.DeleteReaction(reaction.ID);
		}

		public void DeleteReactionFromUserOnPost(AccountDTO account, PostDTO post)
		{
			var reaction =
				reactionService.ListReactions(new ReactionFilter
					{
						Account = account,
						Post = post
					}).ResultReactions.FirstOrDefault();

			if (reaction != null)
			{
				reactionService.DeleteReaction(reaction.ID);
			}
		}

		public void DeleteComment(CommentDTO comment)
		{
			commentService.DeleteComment(comment.ID);
		}

		public int CommentPost(PostDTO post, CommentDTO comment, AccountDTO account)
		{
			comment.Sender = account;
			comment.Post = post;
			return commentService.CreateComment(comment);
		}

		public int ReactOnPost(PostDTO post, ReactionEnum reaction, AccountDTO account)
		{

			return reactionService.CreateReaction(post,reaction,account);
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