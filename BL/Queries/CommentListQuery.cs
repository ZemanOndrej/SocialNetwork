using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.AppRigantiInfrastructure;
using BL.DTO.Filters;
using BL.DTO.PostDTOs;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
	public class CommentListQuery : AppQuery<CommentDTO>
	{
		public CommentListQuery(IUnitOfWorkProvider provider) : base(provider)
		{
		}

		public CommentFilter Filter { get; set; }

		protected override IQueryable<CommentDTO> GetQueryable()
		{
			IQueryable<Comment> query = Context.Comments;

			if (Filter.Post != null)
				query = query.Where(c => c.Post.ID == Filter.Post.ID);
			return query.ProjectTo<CommentDTO>();
		}
	}
}