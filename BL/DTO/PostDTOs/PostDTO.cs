using System;
using System.Collections.Generic;
using BL.DTO.GroupDTOs;
using BL.DTO.UserDTOs;
using Utils.Enums;

namespace BL.DTO
{
	public class PostDTO
	{
		public int ID { get; set; }

		public string Message { get; set; }

		public AccountDTO Sender { get; set; }

		public DateTime Time { get; set; }

		public GroupDTO Group { get; set; }//todo change to groupdto

		public PostPrivacyLevel PrivacyLevel { get; set; }=PostPrivacyLevel.OnlyFriends;


		public override string ToString()
		{
			return Group == null ? $"{Sender.FullName} at  {Time} updated status \n \" {Message}\"" 
								   : $"{Sender.FullName} at  {Time} posted to {Group.Name} groupId\n \" {Message}\"";
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