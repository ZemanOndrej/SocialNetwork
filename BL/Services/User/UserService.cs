using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.GroupDTOs;
using BL.Queries;
using BL.Repositories;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Services.User
{
	public class UserService : AppService,IUserService
	{
		public int UserPageSize => 20;

		#region Dependency

		private readonly UserRepository userRepository;

		private readonly UserListQuery userListQuery;

		public UserService(UserRepository userRepository, UserListQuery userListQuery)
		{
			this.userRepository = userRepository;
			this.userListQuery = userListQuery;
		}

		#endregion

		#region CreateDelete
		public int CreateUser(UserDTO user)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{

				var userEnt = Mapper.Map<DAL.Entities.User>(user);

				userRepository.Insert(userEnt);

				uow.Commit();
				return userEnt.ID;
			}
		}

		public void DeleteUser(int userId)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var user = userRepository.GetById(userId);

				userRepository.Delete(user);

				uow.Commit();
			}
		}



		#endregion

		#region Update
		public void EditUser(UserDTO userDto)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var user = userRepository.GetById(userDto.ID,u=>u.FriendList);
				Mapper.Map(userDto, user);

				userRepository.Update(user);

				uow.Commit();
			}
		}

		public void AddUsersToFriends(int id1, int id2)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var user1 = userRepository.GetById(id1);
				var user2 = userRepository.GetById(id2);

				user1.FriendList.Add(new Friendship(user1, user2));
				userRepository.Update(user1);
				uow.Commit();

			}
		}

		public void AddUsersToFriends(UserDTO user1Dto, UserDTO user2Dto)
		{
			AddUsersToFriends(user1Dto.ID, user2Dto.ID);
		}

		public void RemoveUsersFromFriends(UserDTO userDto, UserDTO user2Dto)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var user1 = userRepository.GetById(userDto.ID,u=>u.FriendList);
				var user2 = userRepository.GetById(user2Dto.ID,u=>u.FriendList);


				var fs = new Friendship(user1, user2);


				user1.FriendList.Remove(fs);
				userRepository.Update(user1);
				uow.Commit();

			}
		}


		#endregion

		#region Get

		public UserDTO GetUserById(int userId)
		{
			using (UnitOfWorkProvider.Create())
			{
				var user = userRepository.GetById(userId,u=>u.FriendList);
				return user != null ? Mapper.Map<UserDTO>(user) : null;
			}
		}

		public UserListQueryResultDTO ListUsers(UserFilter filter, int page = 0)
		{
			using (UnitOfWorkProvider.Create())
			{
				var queery = GetQuery(filter);
				queery.Skip = (page > 0 ? page - 1 : 0) * UserPageSize;
				queery.Take = UserPageSize;
				queery.AddSortCriteria("Surname");


				return new UserListQueryResultDTO
				{
					RequestedPage = page,
					ResultCount = queery.GetTotalRowCount(),
					ResultUsers = queery.Execute(),
					Filter = filter
				};
			}
		}

		public List<UserDTO> ListFriendsOfUser(UserDTO user)
		{
			using (UnitOfWorkProvider.Create())
			{
				var userEnt = userRepository.GetById(user.ID,u=>u.FriendList);

				var retList=new List<UserDTO>();

				foreach (var friendship in userEnt.FriendList)
				{
					DAL.Entities.User tmpUs = null;
					if (friendship.User1.Equals(userEnt)) tmpUs = friendship.User2;
					else if(friendship.User2.Equals(userEnt)) tmpUs = friendship.User1;

					retList.Add(Mapper.Map<DAL.Entities.User, UserDTO>(tmpUs));
				}
				return retList;
			}
		}

		public List<GroupDTO> ListUsersGroups(UserDTO user)
		{
			using (UnitOfWorkProvider.Create())
			{
				var userEnt = userRepository.GetById(user.ID, u => u.Groups);
				return Mapper.Map<List<GroupDTO>>(userEnt.Groups);

			}
		}

		#endregion


		#region additional

		private IQuery<UserDTO> GetQuery(UserFilter filter )
		{
			var query = userListQuery;
			query.ClearSortCriterias();
			query.Filter = filter;
			return query;
		}

		

		#endregion

	}
}
