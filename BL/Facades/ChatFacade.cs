using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.Filters;
using BL.Services.Chat;
using BL.Services.ChatMessage;
using BL.Services.User;

namespace BL.Facades
{
	public class ChatFacade
	{
		#region Dependecy
		private readonly IChatService chatService;
		private readonly IChatMessageService chatMessageService;

		public ChatFacade(IChatService chatService, IChatMessageService chatMessageService)
		{
			this.chatService = chatService;
			this.chatMessageService = chatMessageService;
		}
		#endregion

		public int CreateChat(UserDTO user1, UserDTO user2 ,string name=null)
		{
			if (name == null)
			{
				name = $"{user1.Name} and {user2.Name} chat";
			}			

			return chatService.CreateChat( new ChatDTO { ChatUsers = new List<UserDTO> { user1, user2 },Name = name});
		}

		public int CreateGroupChat(List<UserDTO> users , string name=null)
		{
			if (name != null) return chatService.CreateChat(new ChatDTO {ChatUsers = users, Name = name});
			var strB = new StringBuilder();
			users.ForEach(u=>strB.Append(u.Name+", "));
			name = strB.ToString();
			return chatService.CreateChat(new ChatDTO { ChatUsers = users, Name = name});
		}

		public void DeleteChat(ChatDTO chat)
		{
			chatService.DeleteChat(chat.ID);
		}

		public void DeleteChatMessage(ChatMessageDTO chatMessage)
		{
			chatMessageService.DeleteChatMessage(chatMessage);
		}

		public void AddUserToChat(ChatDTO chat, UserDTO user)
		{
			chatService.AddUserToChat(chat,user);
		}

		public void RemoveUserFromChat(ChatDTO chat, UserDTO user)
		{
			chatService.RemoveUserFromChat(chat , user);
		}

		public void EditChatName(ChatDTO chat , string name)
		{
			chat.Name = name;
			chatService.EditChatName(chat);
		}

		public void EditChatMessage(ChatMessageDTO chatMessage, string message)
		{
			chatMessage.Message = message;
			chatMessageService.EditChatMessage(chatMessage);
		}


		public List<ChatDTO> ListAllUsersChats(UserDTO user, int page = 0)
		{
			return chatService.ListChats(new ChatFilter {User = user}, page).ResultChats.ToList();
		}

		public ChatDTO GetChatById(int id)
		{
			return chatService.GetChatById(id);
		}

		public ChatMessageDTO GetChatMessageById(int id)
		{
			return chatMessageService.GetChatMessageById(id);
		}

		public List<ChatMessageDTO> GetChatMessagesFromChat(ChatDTO chat, int page = 0)
		{
			return chatMessageService.ListChatMessages(new ChatMessageFilter {Chat = chat}, page).ResultMessages.ToList();
		}


		public List<ChatDTO> ListAllChats()
		{
			return chatService.ListChats(new ChatFilter()).ResultChats.ToList();
		}

		public List<UserDTO> ListAllUsersInChat(ChatDTO chat)
		{
			return chatService.GetUsersInChat(chat);
		}


		public List<ChatMessageDTO> ListAllChatMessages()
		{
			return chatMessageService.ListChatMessages(new ChatMessageFilter()).ResultMessages.ToList();
		}

		public int SendChatMessageToChat(ChatDTO chat, UserDTO user, ChatMessageDTO message)
		{
			return chatMessageService.PostMessageToChat(chat,user,message);

		}

	}
}
