using System.Collections.Generic;
using BL.DTO.Request;
using BL.DTO.UserDTOs;

namespace PL.Models
{
	public class RequestModel
	{
		public List<RequestDTO> Requests { get; set; }

	}

	public class InvitePeopleModel
	{
		public int? GroupId { get; set; }
		public int? ChatId { get; set; }
		public List<InviteModel> Invites { get; set; }
	}
	public class InviteModel
	{
		public AccountDTO Account { get; set; }
		public bool Invited { get; set; }
	}

}