using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.DTO.GroupDTOs;

namespace PL.Models
{
	public class FrontPageModel
	{
		public List<PostDTO> Posts { get; set; }
		public PostDTO NewPost { get; set; }

	}

	public class UserPageModel
	{
		public UserDTO User { get; set; }
		public List<PostDTO> Posts { get; set; }
		public PostDTO NewPost { get; set; }

	}

	public class OpenPostModel
	{
		public PostDTO Post { get; set; }
		public List<ReactionDTO> Reactions { get; set; }
		public List<CommentDTO> Comments { get; set; }
		public CommentDTO NewComment { get; set; }
		public ReactionDTO NewReaction { get; set; }

		public int PostId { get; set; }
	}

	public class PostModel
	{
		public PostDTO Post { get; set; }
		public GroupDTO Group { get; set; }
	}
}