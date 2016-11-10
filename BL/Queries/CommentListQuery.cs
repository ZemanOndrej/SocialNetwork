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
	public class CommentListQuery : AppQuery<CommentDTO>
	{
		public CommentFilter Filter { get; set; }
		
		public CommentListQuery(IUnitOfWorkProvider provider) : base(provider)
		{
		}

		protected override IQueryable<CommentDTO> GetQueryable()
		{
			IQueryable<Comment> query = Context.Comments;

			if (Filter.Post != null)
			{
				query = query.Where(c => c.Post.ID == Filter.Post.ID);
			}
			return query.ProjectTo<CommentDTO>();
		}
	}
}
