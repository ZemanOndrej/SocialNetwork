using System.Collections.Generic;
using BL.DTO.Filters;

namespace BL.DTO.ChatDTOs
{
	public class ChatMessageListQueryResultDTO
	{
		public ChatMessageFilter Filter { get; set; }

		public int ResultCount { get; set; }

		public IEnumerable<ChatMessageDTO> ResultMessages { get; set; }

		public int RequestedPage { get; set; }
	}
}