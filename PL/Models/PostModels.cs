using System.Collections.Generic;
using BL.DTO;
using BL.DTO.GroupDTOs;
using BL.DTO.UserDTOs;

namespace PL.Models
{
	

	public class DefaultPageModel
	{
		public bool PendingFriendRequest { get; set; } = false;
		public bool IsYourFriend { get; set; } = false;
		public AccountDTO Account { get; set; }
		public List<PostDTO> Posts { get; set; }
		public PostDTO NewPost { get; set; }
		public List<AccountDTO> Accounts { get; set; }
		public GroupDTO Group { get; set; }


	}


	public class OpenPostModel
	{
		public PostDTO Post { get; set; }
		public List<ReactionDTO> Reactions { get; set; }
		public List<CommentDTO> Comments { get; set; }
		public CommentDTO NewComment { get; set; }
		public ReactionDTO NewReaction { get; set; }
		public string BackView { get; set; }

		public int PostId { get; set; }
	}

	public class PostModel
	{
		public PostDTO Post { get; set; }
		public GroupDTO Group { get; set; }
	}
	public class PostEditModel
	{
		public string BackView { get; set; }
		public PostDTO Post { get; set; }
	}
}