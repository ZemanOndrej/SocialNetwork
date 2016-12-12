using System.Collections.Generic;
using System.Linq;
using BL.DTO.Filters;
using BL.DTO.Request;
using BL.DTO.UserDTOs;
using BL.Services.Group;
using BL.Services.Request;
using BL.Services.User;

namespace BL.Facades
{
	public class RequestFacade
	{
		public int SendRequest(int senderId, int receiverId, int groupId = 0)
		{
			var sender = userService.GetUserById(senderId);
			var receiver = userService.GetUserById(receiverId);

			if (groupId > 0)
			{
				var group = groupService.GetGroupById(groupId);
				var potReq = requestService.ListRequests(new RequestFilter
				{
					Receiver = sender,
					Group = group
				}).ResultRequests.ToList();
				var receiversGroups = userService.ListUsersGroups(receiver);

				if (!potReq.Any() && !receiversGroups.Contains(group))
					return requestService.SendRequest(
						new RequestDTO
						{
							Sender = sender,
							Receiver = receiver,
							Group = group
						});
			}
			else
			{
				var potReq = requestService.ListRequests(new RequestFilter
				{
					Receiver = sender,
					Sender = receiver
				}).ResultRequests.Where(r => r.Group == null).ToList();
				if (!potReq.Any())
					return requestService.SendRequest(
						new RequestDTO
						{
							Sender = sender,
							Receiver = receiver
						});
				AcceptRequest(potReq.First().ID, senderId);
			}

			return 0;
		}

		public void DeleteRequest(int id)
		{
			requestService.DeleteRequest(requestService.GetRequestById(id));
		}

		public RequestDTO GetRequestById(int id)
		{
			return requestService.GetRequestById(id);
		}

		public void AcceptRequest(int requestId, int currAccountId)
		{
			var reqDb = requestService.GetRequestById(requestId);
			var currentAcc = userService.GetUserById(currAccountId);

			if ((reqDb == null) || !currentAcc.Equals(reqDb.Receiver)) return;

			if (reqDb.Group == null)
				userService.AddUsersToFriends(currentAcc, reqDb.Sender);
			else
				groupService.AddUserToGroup(reqDb.Group, currentAcc);

			requestService.DeleteRequest(reqDb);
		}


		public List<RequestDTO> ListRequestsForUserReceiver(AccountDTO account, int page = 0)
		{
			return requestService.ListRequests(new RequestFilter {Receiver = account}, page).ResultRequests.ToList();
		}

		public List<RequestDTO> ListRequestsForUserSender(AccountDTO account, int page = 0)
		{
			return requestService.ListRequests(new RequestFilter {Sender = account}, page).ResultRequests.ToList();
		}

		#region Dependency

		private readonly IRequestService requestService;
		private readonly IUserService userService;
		private readonly IGroupService groupService;

		public RequestFacade(IRequestService requestService, IGroupService groupService, IUserService userService)
		{
			this.requestService = requestService;
			this.groupService = groupService;
			this.userService = userService;
		}

		#endregion
	}
}