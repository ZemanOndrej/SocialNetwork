using System.Collections.Generic;
using BL.DTO.UserDTOs;

namespace BL.DTO
{
	public class ChatDTO
	{
		public ChatDTO()
		{
			ChatUsers = new List<AccountDTO>();
		}

		public List<AccountDTO> ChatUsers { get; set; }


		public string Name { get; set; }


		public int ID { get; set; }

		public override string ToString()
		{
			return $"{nameof(ID)}: {ID}, {nameof(Name)}: {Name}, {nameof(ChatUsers)}Count: {ChatUsers.Count} ";
		}

		protected bool Equals(ChatDTO other)
		{
			return ID == other.ID;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ChatDTO) obj);
		}

		public override int GetHashCode()
		{
			return ID;
		}
	}
}