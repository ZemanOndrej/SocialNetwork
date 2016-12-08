using System;
using BL.DTO.UserDTOs;

namespace BL.DTO.Filters
{
	public class ChatMessageFilter
	{
		public ChatDTO Chat { get; set; }

		public string Text { get; set; }

		public AccountDTO Sender { get; set; }

		//		public DateTime Time { get; set; }

	}
}
