using System;

namespace BL.DTO.Filters
{
	public class ChatMessageFilter
	{
		public ChatDTO Chat { get; set; }

		public string Text { get; set; }

		public UserDTO Sender { get; set; }

		//		public DateTime Time { get; set; }

	}
}
