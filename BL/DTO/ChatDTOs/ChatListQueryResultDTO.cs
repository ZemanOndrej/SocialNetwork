using System.Collections.Generic;
using BL.DTO.Filters;

namespace BL.DTO.ChatDTOs
{
	public class ChatListQueryResultDTO
	{
		public ChatFilter Filter { get; set; }

		public int ResultCount { get; set; }

		public IEnumerable<ChatDTO> ResultChats { get; set; }

		public int RequestedPage { get; set; }
	}
}