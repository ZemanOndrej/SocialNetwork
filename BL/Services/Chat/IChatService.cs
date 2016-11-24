using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.ChatDTOs;
using BL.DTO.Filters;

namespace BL.Services.Chat
{
	public interface IChatService
	{
		int CreateChat(ChatDTO chatDto );

		void DeleteChat(int chatId);


		void EditChatName(ChatDTO chatDto);

		void RemoveUserFromChat(ChatDTO chat, UserDTO user);

		void AddUserToChat(ChatDTO chat ,UserDTO user);


		ChatDTO GetChatById(int id);

		ChatListQueryResultDTO ListChats(ChatFilter filter , int page=0);

		List<UserDTO> GetUsersInChat(ChatDTO chat);



	}
}
