using BL.DTO.UserDTOs;
using Utils.Enums;

namespace BL.DTO.PostDTOs
{
	public class ReactionDTO
	{
		public PostDTO Post { get; set; }

		public AccountDTO Sender { get; set; }

		public ReactionEnum UserReaction { get; set; }

		public int ID { get; set; }

		public override string ToString()
		{
			return $"{nameof(Post)}: {Post}, {nameof(Sender)}: {Sender}, {nameof(UserReaction)}: {UserReaction}";
		}
	}
}