using System.Collections.Generic;
using BL.DTO.GroupDTOs;
using BL.DTO.UserDTOs;

namespace PL.Models
{
	public class SearchModel
	{
		public List<GroupDTO> FoundGroups { get; set; }
		public List<AccountDTO> FoundAccounts { get; set; }
		public string Keyword { get; set; }
	}

	public class SearchForInviteModel
	{
		public int? GroupId { get; set; }
		public int? ChatId { get; set; }
	}

}
