using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.AppRigantiInfrastructure;
using BL.DTO.ChatDTOs;
using BL.DTO.Filters;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
	public class ChatMessageListQuery : AppQuery<ChatMessageDTO>
	{
		public ChatMessageListQuery(IUnitOfWorkProvider provider) : base(provider)
		{
		}

		public ChatMessageFilter Filter { get; set; }

		protected override IQueryable<ChatMessageDTO> GetQueryable()
		{
			IQueryable<ChatMessage> query = Context.ChatMessages;


			if (!string.IsNullOrEmpty(Filter.Text))
				query = query.Where(u => u.Message.ToLower()
					.Contains(Filter.Text.ToLower()));

			if (Filter.Sender != null)
				query = query.Where(u => u.ID == Filter.Sender.ID);
			if (Filter.Chat != null)
				query = query.Where(c => c.Chat.ID == Filter.Chat.ID);

			return query.ProjectTo<ChatMessageDTO>();
		}
	}
}