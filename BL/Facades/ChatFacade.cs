using System.Collections.Generic;
using System.Linq;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.UserDTOs;
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
		private readonly IUserService userService;

		public ChatFacade(IChatService chatService, IChatMessageService chatMessageService, IUserService userService)
		{
			this.chatService = chatService;
			this.chatMessageService = chatMessageService;
			this.userService = userService;
		}
		#endregion

		public int CreateChat(AccountDTO user1, AccountDTO user2 ,string name=null)
		{
			if (name == null)
			{
				name = $"{user1.Name} and {user2.Name} chat";
			}

			return chatService.CreateChat( new ChatDTO { ChatUsers = new List<AccountDTO> { user1, user2 },Name = name});
		}

		public int CreateGroupChat(List<AccountDTO> users , string name=null)
		{
			return chatService.CreateChat(name != null ?
				new ChatDTO {ChatUsers = users, Name = name} : new ChatDTO { ChatUsers = users});
		}

		public void DeleteChat(ChatDTO chat)
		{
			chatService.DeleteChat(chat.ID);
		}

		public void DeleteChatMessage(ChatMessageDTO chatMessage)
		{
			chatMessageService.DeleteChatMessage(chatMessage);
		}

		public int AddUserToChat(ChatDTO chat, AccountDTO account)
		{
			var chatUsers = chatService.GetUsersInChat(chat);

			if (chatUsers.Count <= 2)
			{
				chat.ID = 0;
				chat.ChatUsers.Add(account);
				return chatService.CreateChat(chat);
			}
			chatService.AddUserToChat(chat,account);
			return 0;
		}

		public int AddUsersToChat(ChatDTO chat, List<AccountDTO> accounts)
		{
			var chatUsers = chatService.GetUsersInChat(chat);

			if (chatUsers.Count <= 2)
			{
				chat.ID = 0;
				chat.Name = null;
				chat.ChatUsers.AddRange(accounts);
				return chatService.CreateChat(chat);
			}

			foreach (var account in accounts)
			{
				chatService.AddUserToChat(chat, account);
				
			}
			return 0;
		}

		public void RemoveUserFromChat(ChatDTO chat, AccountDTO account)
		{
			chatService.RemoveUserFromChat(chat , account);
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


		public List<ChatDTO> ListAllUsersChats(int accountId, int page = 0)
		{
			var account = userService.GetUserById(accountId);
			return ListAllUsersChats(account, page);
		}

		public List<ChatDTO> ListAllUsersChats(AccountDTO account, int page = 0)
		{
			return chatService.ListChats(new ChatFilter {Account = account}, page).ResultChats.ToList();
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

		public List<AccountDTO> ListAllUsersInChat(ChatDTO chat)
		{
			return chatService.GetUsersInChat(chat);
		}


		public List<ChatMessageDTO> ListAllChatMessages()
		{
			return chatMessageService.ListChatMessages(new ChatMessageFilter()).ResultMessages.ToList();
		}

		public int SendChatMessageToChat(ChatDTO chat, AccountDTO account, ChatMessageDTO message)
		{
			return chatMessageService.PostMessageToChat(chat,account,message);

		}

	}
}
