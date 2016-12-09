using System;
using System.Collections.Generic;
using BL.DTO.UserDTOs;
using Utils.Enums;

namespace BL.DTO.GroupDTOs
{
	public class GroupDTO
	{
		public GroupDTO()
		{
			Accounts = new List<AccountDTO>();
		}

		public string Name { get; set; }
		public string Description { get; set; }
		public GroupPrivacyLevel GroupPrivacyLevel { get; set; }=GroupPrivacyLevel.Private;
		public DateTime DateCreated { get; set; }
		public List<AccountDTO> Accounts { get; set; }

		public override string ToString()
		{
			return $"{nameof(DateCreated)}: {DateCreated}, {nameof(Description)}: {Description}, {nameof(Name)}: {Name}";
		}

		public int ID { get; set; }
	}
}