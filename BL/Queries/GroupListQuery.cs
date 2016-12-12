using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.AppRigantiInfrastructure;
using BL.DTO.Filters;
using BL.DTO.GroupDTOs;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
	public class GroupListQuery : AppQuery<GroupDTO>
	{
		public GroupListQuery(IUnitOfWorkProvider provider) : base(provider)
		{
		}

		public GroupFilter Filter { get; set; }

		protected override IQueryable<GroupDTO> GetQueryable()
		{
			IQueryable<Group> query = Context.Groups;

			if (!string.IsNullOrEmpty(Filter?.Name))
				query = query.Where(group => group.Name.ToLower()
					.Contains(Filter.Name.ToLower()));

			return query.ProjectTo<GroupDTO>();
		}
	}
}