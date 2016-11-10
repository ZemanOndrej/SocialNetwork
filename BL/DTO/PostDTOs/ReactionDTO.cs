using Utils.Enums;

namespace BL.DTO
{
	public class ReactionDTO
	{
		public PostDTO Post { get; set; }

		public UserDTO Sender { get; set; }

		public ReactionEnum UserReaction { get; set; }

		public int ID { get; set; }

		public override string ToString()
		{
			return $"{nameof(Post)}: {Post}, {nameof(Sender)}: {Sender}, {nameof(UserReaction)}: {UserReaction}";
		}
	}
}