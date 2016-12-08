using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BL.AppRigantiInfrastructure;
using BL.DTO.Filters;
using BL.DTO.UserDTOs;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
	public class FriendListQuery : AppQuery<FriendshipDTO>
	{
		public FriendListQuery(IUnitOfWorkProvider provider) : base(provider){}

		public FriendshipFilter Filter { get; set; }

		protected override IQueryable<FriendshipDTO> GetQueryable()
		{
			IQueryable<Friendship> query = Context.Friendships;
			if (Filter.Account != null && Filter.Account2==null)
			{
				query = query.Where(f => f.User1.ID == Filter.Account.ID || f.User2.ID == Filter.Account.ID);
			}
			else if(Filter.Account != null && Filter.Account2 != null)
			{
				query = query.Where(f =>
					   f.User1.ID.Equals(Filter.Account.ID) && f.User2.ID.Equals(Filter.Account2.ID)
					|| f.User2.ID.Equals(Filter.Account.ID) && f.User1.ID.Equals(Filter.Account2.ID));
			}

			return query.ProjectTo<FriendshipDTO>();
		}
	}
}
