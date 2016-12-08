using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.GroupDTOs;
using BL.DTO.UserDTOs;
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

//		public int CreateNewUser(UserDTO user)
//		{
//			return userService.Register(user);
//		}
		public ClaimsIdentity Login(string email, string password)
		{
			return userService.Login(email, password);
		}
		public int CreateNewUser(AccountDTO account)
		{
			return userService.Register(account);
		}

		public void RemoveUser(AccountDTO account)
		{
			userService.DeleteUser(account.ID);
		}

		public void UpdateUserInfo(AccountDTO account)
		{
			userService.EditUser(account);
		}

		public AccountDTO GetUserByEmail(string email)
		{
			return userService.ListUsers(new UserFilter { Email = email }).ResultUsers.FirstOrDefault();
		}

		public AccountDTO GetUserById(int id)
		{
			return userService.GetUserById(id);
		}


		public void RemoveUsersFromFriends(int id1, int id2)
		{
			userService.RemoveUsersFromFriends(userService.GetUserById(id1),userService.GetUserById(id2));
		}

		public List<AccountDTO> GetUsersWithName(string str, int page = 0)
		{
			return userService.ListUsers(new UserFilter
			{
				Fullname = str,
			}, page).ResultUsers.ToList();
		}

		public List<AccountDTO> ListFriendsOf(AccountDTO account)
		{
			return userService.ListFriendsOfUser(account);

		}
		//page fix
		public List<GroupDTO> ListGroupsWithUser(AccountDTO account)
		{
			return userService.ListUsersGroups(account);
		}


		public List<AccountDTO> ListAllUsers()
		{
			return userService.ListUsers(new UserFilter()).ResultUsers.ToList();
		}

		public void AddUsersToFriends(AccountDTO account, AccountDTO account2)
		{
			userService.AddUsersToFriends(account,account2);
		}
	}
}
