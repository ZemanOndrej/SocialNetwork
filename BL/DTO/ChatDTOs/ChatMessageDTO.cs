using System;
using BL.DTO.UserDTOs;

namespace BL.DTO
{
	public class ChatMessageDTO
	{
		public string Message { get; set; }

		public DateTime Time { get; set; }

		public AccountDTO Sender { get; set; }

		public ChatDTO Chat { get; set; }

		public int ID { get; set; }


		public override string ToString()
		{
			return $"{Sender.FullName} at {Time} to {Chat.Name} sent :\n\" {Message}\"";
		}

		protected bool Equals(ChatMessageDTO other)
		{
			return Time.Equals(other.Time) && ID == other.ID;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((ChatMessageDTO) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Time.GetHashCode()*397) ^ ID;
			}
		}
	}
}