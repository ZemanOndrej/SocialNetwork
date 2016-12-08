using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.ChatDTOs;
using BL.DTO.Filters;
using BL.DTO.UserDTOs;

namespace BL.Services.ChatMessage
{
	public interface IChatMessageService
	{

		int PostMessageToChat(ChatDTO chat, AccountDTO account, ChatMessageDTO message);

		void EditChatMessage(ChatMessageDTO messageDto);

		void DeleteChatMessage(int messageId);

		void DeleteChatMessage(ChatMessageDTO chatMessage);

		ChatMessageDTO GetChatMessageById(int id);

		ChatMessageListQueryResultDTO ListChatMessages(ChatMessageFilter filter, int page = 0);



	}

}
