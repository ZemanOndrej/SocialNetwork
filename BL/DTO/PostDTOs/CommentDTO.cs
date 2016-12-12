using System;
using BL.DTO.UserDTOs;

namespace BL.DTO.PostDTOs
{
	public class CommentDTO
	{
		public string CommentMessage { get; set; }

		public DateTime Time { get; set; }

		public AccountDTO Sender { get; set; }

		public PostDTO Post { get; set; }


		public int ID { get; set; }

		public override string ToString()
		{
			return $"{Sender.FullName} commented on postId: {Post.ID} at {Time} :\n \"{CommentMessage}\" ";
		}
	}
}