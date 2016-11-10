using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.ChatDTOs;
using BL.DTO.Filters;

namespace BL.Services.ChatMessage
{
	public interface IChatMessageService
	{

		int PostMessageToChat(ChatDTO chat, UserDTO user, ChatMessageDTO message);

		void EditChatMessage(ChatMessageDTO messageDto);

		void DeleteChatMessage(int messageId);

		void DeleteChatMessage(ChatMessageDTO chatMessage);

		ChatMessageDTO GetChatMessageById(int id);

		ChatMessageListQueryResultDTO ListChatMessages(ChatMessageFilter filter, int page = 0);



	}

}
