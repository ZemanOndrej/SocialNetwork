using System.Collections.Generic;
using BL.DTO.ChatDTOs;
using BL.DTO.Filters;
using BL.DTO.UserDTOs;

namespace BL.Services.Chat
{
	public interface IChatService
	{
		int CreateChat(ChatDTO chatDto);

		void DeleteChat(int chatId);


		void EditChatName(ChatDTO chatDto);

		void RemoveUserFromChat(ChatDTO chat, AccountDTO account);

		void AddUserToChat(ChatDTO chat, AccountDTO account);
		void AddUsersToChat(ChatDTO chat, List<AccountDTO> accounts);

		ChatDTO GetChatById(int id);

		ChatListQueryResultDTO ListChats(ChatFilter filter, int page = 0);

		List<AccountDTO> GetUsersInChat(ChatDTO chat);
	}
}