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
	public class ReactionListQuery :AppQuery<ReactionDTO>
	{

		public ReactionFilter Filter { get; set; }

		public ReactionListQuery(IUnitOfWorkProvider provider) : base(provider)
		{
		}

		protected override IQueryable<ReactionDTO> GetQueryable()
		{
			IQueryable<Reaction> query = Context.Reactions;

			if (Filter.Post != null)
			{
				query = query.Where(c => c.Post.ID == Filter.Post.ID);
			}
			if (Filter.Account!=null)
			{
				query = query.Where(r => r.Account.ID.Equals(Filter.Account.ID));
			}
			return query.ProjectTo<ReactionDTO>();
		}
	}
}
