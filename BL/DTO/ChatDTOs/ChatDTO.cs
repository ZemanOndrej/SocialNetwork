using System.Collections.Generic;

namespace BL.DTO
{
	public class ChatDTO
	{
		public ChatDTO()
		{
			ChatUsers = new List<UserDTO>();
		}

		public List<UserDTO> ChatUsers { get; set; }


		public string Name { get; set; }


		public int ID { get; set; }

		public override string ToString()
		{
			return $"{nameof(ID)}: {ID}, {nameof(Name)}: {Name}, {nameof(ChatUsers)}Count: {ChatUsers.Count} ";
		}
	}
}