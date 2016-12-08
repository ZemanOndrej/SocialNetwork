using System;
using System.Collections.Generic;
using BL.DTO.UserDTOs;

namespace BL.DTO.GroupDTOs
{
	public class GroupDTO
	{
		public GroupDTO()
		{
			Accounts = new List<AccountDTO>();
//			GroupPosts = new List<PostDTO>();
		}

		public string Name { get; set; }

		public DateTime DateCreated { get; set; }
		public List<AccountDTO> Accounts { get; set; }
//		public List<PostDTO> GroupPosts { get; set; }

		public override string ToString()
		{
			return $"{nameof(DateCreated)}: {DateCreated}, {nameof(Name)}: {Name}";
		}

		public int ID { get; set; }
	}
}