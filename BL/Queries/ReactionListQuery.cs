using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.AppRigantiInfrastructure;
using BL.DTO.Filters;
using BL.DTO.PostDTOs;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
	public class ReactionListQuery : AppQuery<ReactionDTO>
	{
		public ReactionListQuery(IUnitOfWorkProvider provider) : base(provider)
		{
		}

		public ReactionFilter Filter { get; set; }

		protected override IQueryable<ReactionDTO> GetQueryable()
		{
			IQueryable<Reaction> query = Context.Reactions;

			if (Filter.Post != null)
				query = query.Where(c => c.Post.ID == Filter.Post.ID);
			if (Filter.Account != null)
				query = query.Where(r => r.Account.ID.Equals(Filter.Account.ID));
			return query.ProjectTo<ReactionDTO>();
		}
	}
}