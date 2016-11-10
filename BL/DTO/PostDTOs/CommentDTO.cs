﻿using System;
using System.Collections.Generic;

namespace BL.DTO
{
	public class CommentDTO
	{
		public CommentDTO()
		{
//			Reactions = new List<ReactionDTO>();
		}
		public string CommentMessage { get; set; }

		public DateTime Time { get; set; }

		public UserDTO Sender { get; set; }

		public PostDTO Post { get; set; }


		public int ID { get; set; }

		public override string ToString()
		{
			return $"{Sender.FullName} commented on postId: {Post.ID} at {Time} :\n \"{CommentMessage}\" ";
		}
	}
}