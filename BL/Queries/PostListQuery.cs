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
	public class PostListQuery :AppQuery<PostDTO>
	{

		public PostFilter Filter { get; set; }

		public PostListQuery(IUnitOfWorkProvider provider) : base(provider) { }


		protected override IQueryable<PostDTO> GetQueryable()
		{
			IQueryable<Post> query = Context.Posts;

			if (Filter.Group != null)
			{
				query = query.Where(p => p.Group.ID == Filter.Group.ID);
			}
			if (Filter.Message != null)
			{
				query = query.Where(u => u.Message.ToLower()
					.Contains(Filter.Message.ToLower()));
			}
			if (Filter.Sender != null)
			{
				query = query.Where(u => u.Sender.ID == Filter.Sender.ID);

			}
			if (Filter.PrivacyLevel != null)
			{
				query = query.Where(u => u.PrivacyLevel == Filter.PrivacyLevel);

			}

			return query.ProjectTo<PostDTO>();
		}
	}
}
