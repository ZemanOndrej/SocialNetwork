using System.Linq;
using AutoMapper.QueryableExtensions;
using BL.AppRigantiInfrastructure;
using BL.DTO.Filters;
using BL.DTO.Request;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
	public class RequestListQuery : AppQuery<RequestDTO>
	{
		public RequestListQuery(IUnitOfWorkProvider provider) : base(provider)
		{
		}

		public RequestFilter Filter { get; set; }

		protected override IQueryable<RequestDTO> GetQueryable()
		{
			IQueryable<Request> query = Context.Requests;


			if (Filter.Group != null)
				query = query.Where(u => u.Group.ID == Filter.Group.ID);
			if (Filter.Sender != null)
				query = query.Where(q => q.Sender.ID == Filter.Sender.ID);
			if (Filter.Receiver != null)
				query = query.Where(q => q.Receiver.ID == Filter.Receiver.ID);

			return query.ProjectTo<RequestDTO>();
		}
	}
}