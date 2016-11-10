using System;
using System.Collections.Generic;
using Utils.Enums;

namespace BL.DTO
{
	public class PostDTO
	{
		public PostDTO()
		{
//			Comments = new List<CommentDTO>();
//			Reactions = new List<ReactionDTO>();
		}
		public int ID { get; set; }

		public string Message { get; set; }

		public UserDTO Sender { get; set; }

		public DateTime Time { get; set; }

		public int? GroupId { get; set; }//TODO new entity mapping

		public PostPrivacyLevel PrivacyLevel { get; set; }=PostPrivacyLevel.OnlyFriends;


		//		public GroupDTO Group { get; set; }

		//		public List<CommentDTO> Comments { get; set; }//TODO vymazat tieto Property
		//		public List<ReactionDTO> Reactions { get; set; }

		public override string ToString()
		{
			return $"{Sender.FullName} at  {Time} posted to {GroupId} groupId\n \" {Message}\"";
		}

		protected bool Equals(PostDTO other)
		{
			return string.Equals(Message, other.Message) && Equals(Sender, other.Sender) && Time.Equals(other.Time);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((PostDTO) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Message?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ (Sender?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ Time.GetHashCode();
				return hashCode;
			}
		}
	}
}