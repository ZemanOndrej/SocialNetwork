using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using BL.AppRigantiInfrastructure;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.GroupDTOs;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
	public class GroupListQuery : AppQuery<GroupDTO>
	{
		public GroupFilter Filter { get; set; }

		public GroupListQuery(IUnitOfWorkProvider provider) : base(provider){}

		protected override IQueryable<GroupDTO> GetQueryable()
		{
			IQueryable<Group> query = Context.Groups;

			if (!string.IsNullOrEmpty(Filter?.Name))
			{

				query = query.Where(group => group.Name.ToLower()
				.Contains(Filter.Name.ToLower()));
			}
			
			return query.ProjectTo<GroupDTO>();
		}
	}
}
