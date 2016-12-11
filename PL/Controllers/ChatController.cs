using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Microsoft.AspNet.Identity;
using PL.Models;

namespace PL.Controllers
{
	[Authorize]
	public class ChatController : Controller
	{
		#region Dependency
		private readonly ChatFacade chatFacade;
		private readonly UserFacade userFacade;

		public ChatController(UserFacade userFacade, ChatFacade chatFacade)
		{
			this.userFacade = userFacade;
			this.chatFacade = chatFacade;
		}
		#endregion

		public ActionResult Index()
		{
			var usersChats = chatFacade.ListAllUsersChats(int.Parse(User.Identity.GetUserId()));
			
			return View(new ChatListModel {Chats = usersChats});
		}

		[HttpPost]
		public ActionResult Create(int id)
		{
			var chatId =chatFacade.CreateChat(
				userFacade.GetUserById(int.Parse(User.Identity.GetUserId())),
				userFacade.GetUserById(id));
			return RedirectToAction("OpenChat", new {id = chatId});
		}

		public ActionResult Delete(int id)
		{
			var chat = chatFacade.GetChatById(id);
			var user = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));
			if (!chatFacade.ListAllUsersInChat(chat).Contains(user)) return RedirectToAction("AccessDenied", "Page");

			return View(chat);


		}

		[HttpPost]
		public ActionResult Delete(ChatDTO chat)
		{
			chatFacade.DeleteChat(chat);

			return RedirectToAction("Index");


		}

		public ActionResult OpenChat(int id , int page =1 )
		{
			var chat = chatFacade.GetChatById(id);
			var userList = chatFacade.ListAllUsersInChat(chat);
			var user = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));
			if (!userList.Contains(user))
			{
				return RedirectToAction("AccessDenied", "Page");
			}

			var list = chatFacade.GetChatMessagesFromChat(chat, page);
			list.Reverse();
			return View(new OpenChatModel
			{
				Chat = chat , ChatMessages =list ,ChatId = chat.ID, Page = page, Accounts = userList
			});
		}

		[HttpPost]
		public ActionResult OpenChat(OpenChatModel model)
		{
			var user = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));
			var chat = chatFacade.GetChatById(model.ChatId);
			chatFacade.SendChatMessageToChat(chat, user, model.NewChatMessage);
			return RedirectToAction("OpenChat",new {id=chat.ID});
		}

		public ActionResult DeleteMessage(int id)
		{
			var msg = chatFacade.GetChatMessageById(id);
			if (msg.Sender.ID != int.Parse(User.Identity.GetUserId())) return RedirectToAction("AccessDenied", "Page");

			return View(new ChatMessageModel {ChatMessage = msg,ChatId = msg.Chat.ID});
		}

		[HttpPost]
		public ActionResult DeleteMessage(ChatMessageModel model)
		{
			chatFacade.DeleteChatMessage(model.ChatMessage);
			return RedirectToAction("OpenChat", new {id = model.ChatId});
		}

		public ActionResult EditMessage(int id)
		{
			var msg = chatFacade.GetChatMessageById(id);
			if (msg.Sender.ID != int.Parse(User.Identity.GetUserId())) return RedirectToAction("AccessDenied", "Page");

			return View(new ChatMessageModel { ChatMessage = msg, ChatId = msg.Chat.ID });

		}

		[HttpPost]
		public ActionResult EditMessage(ChatMessageModel model)
		{
			chatFacade.EditChatMessage(model.ChatMessage,model.ChatMessage.Message);
			return RedirectToAction("OpenChat", new { id = model.ChatId });

		}

		public ActionResult Edit(int id)
		{
			var chat = chatFacade.GetChatById(id);
			var user = userFacade.GetUserById(int.Parse(User.Identity.GetUserId()));
			
			if (!chatFacade.ListAllUsersInChat(chat).Contains(user)) return RedirectToAction("AccessDenied", "Page");

			return View(chat);
		}
	}
}
