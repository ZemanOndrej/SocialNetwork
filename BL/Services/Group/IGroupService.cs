using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.GroupDTOs;
using BL.DTO.UserDTOs;
using BL.Queries;

namespace BL.Services.Group
{
	public interface IGroupService
	{
		int CreateGroup(GroupDTO group);

		void DeleteGroup(int id);

		void DeleteGroup(GroupDTO group);


		void EditGroupName(GroupDTO group);

		void AddUserToGroup( GroupDTO group,AccountDTO account);

		void RemoveUserFromGroup( GroupDTO group,AccountDTO account);
		List<AccountDTO> ListUsersInGroup(GroupDTO group);


		GroupDTO GetGroupById(int id);

		GroupListQueryResultDTO ListGroups(GroupFilter filter, int page = 0);



	}
}
