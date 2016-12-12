using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.AppRigantiInfrastructure;
using BL.DTO.ChatDTOs;
using BL.DTO.Filters;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
	public class ChatListQuery : AppQuery<ChatDTO>
	{
		public ChatListQuery(IUnitOfWorkProvider provider) : base(provider)
		{
		}


		public ChatFilter Filter { get; set; }

		protected override IQueryable<ChatDTO> GetQueryable()
		{
			IQueryable<Chat> query = Context.ChatSet;


			if (!string.IsNullOrEmpty(Filter.Name))
				query = query.Where(u => u.Name.ToLower()
					.Contains(Filter.Name.ToLower()));

			if (Filter.Account != null)
				query = query.Where(u => u.ChatUsers
					.Contains(Context.Accounts
						.FirstOrDefault(user => user.ID == Filter.Account.ID)));
			return query.ProjectTo<ChatDTO>();
		}
	}
}