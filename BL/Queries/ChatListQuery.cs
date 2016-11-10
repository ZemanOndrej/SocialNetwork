using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using BL.AppRigantiInfrastructure;
using BL.DTO;
using BL.DTO.Filters;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
	public class ChatListQuery : AppQuery<ChatDTO>
	{


		public ChatFilter Filter { get; set; }


		public ChatListQuery(IUnitOfWorkProvider provider) : base(provider) { }

		protected override IQueryable<ChatDTO> GetQueryable()
		{

			IQueryable<Chat> query = Context.ChatSet;



			if (!string.IsNullOrEmpty(Filter.Name))
			{
				query = query.Where(u => u.Name.ToLower()
				.Contains(Filter.Name.ToLower()));
			}

			if (Filter.User != null)
			{
				query = query.Where(u => u.ChatUsers
				.Contains(Context.Users
					.FirstOrDefault(user => user.ID == Filter.User.ID)));
			}
			return query.ProjectTo<ChatDTO>();
		}


	}
}
