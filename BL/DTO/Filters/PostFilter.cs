using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO.GroupDTOs;
using Utils.Enums;

namespace BL.DTO.Filters
{
	public class PostFilter
	{
		public string Message { get; set; }

		public UserDTO Sender { get; set; }

//		public DateTime? Time { get; set; }

		public PostPrivacyLevel? PrivacyLevel { get; set; }

		public GroupDTO Group { get; set; }
	}
}
