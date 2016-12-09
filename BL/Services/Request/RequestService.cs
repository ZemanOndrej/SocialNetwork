using System;
using System.Collections.Generic;
using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.GroupDTOs;
using BL.DTO.Request;
using BL.Queries;
using BL.Repositories;
using BrockAllen.MembershipReboot.Ef;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Services.Request
{
	public class RequestService : AppService,IRequestService
	{

		public int RequestPageSize => 20;

		#region Dependency

		private readonly RequestRepository requestRepository;
		private readonly UserRepository userRepository;
		private readonly GroupRepository groupRepository;
		private readonly RequestListQuery requestListQuery;


		public RequestService(RequestRepository requestRepository, UserRepository userRepository, GroupRepository groupRepository, RequestListQuery requestListQuery)
		{
			this.requestRepository = requestRepository;
			this.userRepository = userRepository;
			this.groupRepository = groupRepository;
			this.requestListQuery = requestListQuery;
		}

		#endregion

		public int SendRequest(RequestDTO request)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{

				var req = new DAL.Entities.Request
				{
					Time = DateTime.Now,
					Receiver = userRepository.GetById(request.Receiver.ID),
					Sender = userRepository.GetById(request.Sender.ID)
				};
				if (request.Group!=null)
				{

					req.Group = groupRepository.GetById(request.Group.ID);
				}

				requestRepository.Insert(req);
				uow.Commit();
				return req.ID;
			}
		}


		public RequestListQueryResultDTO ListRequests(RequestFilter filter, int page=0)
		{
			using (UnitOfWorkProvider.Create())
			{
				var query = GetQuery(filter);
				query.Skip = (page > 0 ? page - 1 : 0) * RequestPageSize;
				query.Take = RequestPageSize;

				query.AddSortCriteria(customer => customer.Time, SortDirection.Descending);


				return new RequestListQueryResultDTO
				{
					RequestedPage = page,
					ResultCount = query.GetTotalRowCount(),
					ResultRequests = query.Execute(),
					Filter = filter
				};

			}
		}

		public RequestDTO GetRequestById(int id)
		{
			using (UnitOfWorkProvider.Create())
			{
				var request = requestRepository.GetById(id);
				return request != null ? Mapper.Map<RequestDTO>(request) : null;
			}
		}


		public void DeleteRequest(RequestDTO request)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{

				var req = requestRepository.GetById(request.ID);
				requestRepository.Delete(req);
				uow.Commit();
			}
		}



		#region additional

		private IQuery<RequestDTO> GetQuery(RequestFilter filter)
		{
			var query = requestListQuery;
			query.ClearSortCriterias();
			query.Filter = filter;
			return query;
		}

		#endregion
	}
}