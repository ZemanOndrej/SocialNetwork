using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.GroupDTOs;

namespace BL.Services.User
{
	public interface IUserService
	{

		int CreateUser(UserDTO user);
		void DeleteUser(int id);

		void EditUser(UserDTO user);
		void AddUsersToFriends(UserDTO user1Dto, UserDTO user2Dto);
		void AddUsersToFriends(int id1, int id2);
		void RemoveUsersFromFriends(UserDTO userDto, UserDTO user2Dto);


		List<GroupDTO> ListUsersGroups(UserDTO user);
		UserDTO GetUserById(int id);
		UserListQueryResultDTO ListUsers(UserFilter filter, int page = 0);
		List<UserDTO> ListFriendsOfUser(UserDTO user);



	}
}
