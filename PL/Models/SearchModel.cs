using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
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
}
