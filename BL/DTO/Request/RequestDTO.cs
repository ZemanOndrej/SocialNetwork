using System;
using BL.DTO.GroupDTOs;
using BL.DTO.UserDTOs;

namespace BL.DTO.Request
{
	public class RequestDTO
	{
		public int ID { get; set; }

		public AccountDTO Sender { get; set; }

		public AccountDTO Receiver { get; set; }

		public GroupDTO Group { get; set; }
		public DateTime Time { get; set; }
	}
}