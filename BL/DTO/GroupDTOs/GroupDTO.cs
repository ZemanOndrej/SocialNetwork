using System;
using System.Collections.Generic;

namespace BL.DTO
{
	public class GroupDTO
	{
		public GroupDTO()
		{
			Accounts = new List<UserDTO>();
//			GroupPosts = new List<PostDTO>();
		}

		public string Name { get; set; }

		public DateTime DateCreated { get; set; }
		public List<UserDTO> Accounts { get; set; }
//		public List<PostDTO> GroupPosts { get; set; }

		public override string ToString()
		{
			return $"{nameof(DateCreated)}: {DateCreated}, {nameof(Name)}: {Name}";
		}

		public int ID { get; set; }
	}
}