using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTO;

namespace PL.Models
{
	public class OpenChatModel
	{
		public ChatDTO Chat { get; set; }

		public List<ChatMessageDTO> ChatMessages { get; set; }

		public ChatMessageDTO NewChatMessage { get; set; }

		public int ChatId { get; set; }
	}

	public class ChatListModel
	{
		public List<ChatDTO> Chats { get; set; }
	}

	public class ChatMessageModel
	{
		public ChatMessageDTO ChatMessage { get; set; }
		public int ChatId { get; set; }
	}
}