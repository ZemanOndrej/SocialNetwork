using System.Collections.Generic;
using System.Linq;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.GroupDTOs;
using BL.Services.Group;
using BL.Services.User;

namespace BL.Facades
{
	public class UserFacade
	{
		#region Dependency
		private readonly IUserService userService;
		private readonly IGroupService groupService;

		public UserFacade(IUserService userService, IGroupService groupService)
		{
			this.userService = userService;
			this.groupService = groupService;
		}
		#endregion

		public int CreateNewUser(UserDTO user)
		{
			return userService.CreateUser(user);
		}

		public void RemoveUser(UserDTO user)
		{
			userService.DeleteUser(user.ID);
		}

		public void UpdateUserInfo(UserDTO user)
		{
			userService.EditUser(user);
		}

		public UserDTO GetUserByEmail(string email)
		{
			return userService.ListUsers(new UserFilter { Email = email }).ResultUsers.FirstOrDefault();
		}

		public UserDTO GetUserById(int id)
		{
			return userService.GetUserById(id);
		}


		public void AddUsersToFriends(UserDTO user1, UserDTO user2)
		{
			userService.AddUsersToFriends(user1,user2);
		}

		public void RemoveUsersFromFriends(UserDTO user, UserDTO user2)
		{
			userService.RemoveUsersFromFriends(user,user2);
		}

		public List<UserDTO> GetUsersWithName(string str, int page = 0)
		{
			return userService.ListUsers(new UserFilter
			{
				Fullname = str,
			}, page).ResultUsers.ToList();
		}

		public List<UserDTO> ListFriendsOf(UserDTO user)
		{
			return userService.ListFriendsOfUser(user);
		}
		//page fix
		public List<GroupDTO> ListGroupsWithUser(UserDTO user)
		{
			return userService.ListUsersGroups(user);
		}


		public List<UserDTO> ListAllUsers()
		{
			return userService.ListUsers(new UserFilter()).ResultUsers.ToList();
		}
	}
}
