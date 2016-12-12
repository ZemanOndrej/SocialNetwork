using System.Collections.Generic;
using System.Security.Claims;
using BL.DTO.Filters;
using BL.DTO.GroupDTOs;
using BL.DTO.UserDTOs;

namespace BL.Services.User
{
	public interface IUserService
	{
//		int Register(UserDTO user);
		ClaimsIdentity Login(string email, string password);
		int Register(AccountDTO account);
		void DeleteUser(int id);

		void EditUser(AccountDTO account);
		void AddUsersToFriends(AccountDTO user1Dto, AccountDTO user2Dto);
		void AddUsersToFriends(int id1, int id2);
		void RemoveUsersFromFriends(AccountDTO accountDto, AccountDTO user2Dto);
		void RemoveFriendship(FriendshipDTO friendship);


		List<GroupDTO> ListUsersGroups(AccountDTO account);
		AccountDTO GetUserById(int id);
		UserListQueryResultDTO ListUsers(UserFilter filter, int page = 0);
		List<AccountDTO> ListFriendsOfUser(AccountDTO account, int page = 0);
	}
}