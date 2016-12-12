using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using BL.DTO.Filters;
using BL.DTO.GroupDTOs;
using BL.DTO.UserDTOs;
using BL.Identity;
using BL.Queries;
using BL.Repositories;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Services.User
{
	public class UserService : AppService, IUserService
	{
		public int UserPageSize => 20;

		#region Dependency

		private readonly UserRepository userRepository;
		private readonly Func<AppRoleManager> roleManagerFactory;
		private readonly FriendshipRepository friendshipRepository;

		private readonly UserListQuery userListQuery;
		private readonly FriendListQuery friendListQuery;

		private readonly Func<AppUserManager> userManagerFactory;

		public UserService(UserRepository userRepository, UserListQuery userListQuery, Func<AppUserManager> userManagerFactory,
			Func<AppRoleManager> roleManagerFactory, FriendshipRepository friendshipRepository, FriendListQuery friendListQuery)
		{
			this.userRepository = userRepository;
			this.userListQuery = userListQuery;
			this.userManagerFactory = userManagerFactory;
			this.roleManagerFactory = roleManagerFactory;
			this.friendshipRepository = friendshipRepository;
			this.friendListQuery = friendListQuery;
		}

		#endregion

		#region CreateDelete

		public int Register(UserDTO user)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var userManager = userManagerFactory.Invoke();

				var userEnt = Mapper.Map<DAL.Entities.Identity.User>(user);

//				userRepository.Insert(userEnt);
				userManager.Create(userEnt);

				uow.Commit();
				return userEnt.Id;
			}
		}

		public int Register(AccountDTO account)
		{
			int id;
			var userManager = userManagerFactory.Invoke();

			using (var uow = UnitOfWorkProvider.Create())
			{
				var userEnt = Mapper.Map<Account>(account);

				var user = new UserDTO
				{
					Email = account.Email,
					UserName = account.UserName,
					Password = account.Password,
					Account = account
				};

				var appUser = Mapper.Map<DAL.Entities.Identity.User>(user);
				appUser.Account = Mapper.Map<Account>(account);
				userManager.Create(appUser, user.Password);

				uow.Commit();
				id = appUser.Id;
			}
			if (id > 0)
				userManager.AddToRole(id, "user");


			return id;
		}

		public ClaimsIdentity Login(string email, string password)
		{
			using (UnitOfWorkProvider.Create())
			{
				using (var userManager = userManagerFactory.Invoke())
				{
					var wantedUser = userManager.FindByEmail(email);

					if (wantedUser == null)
						return null;

					var user = userManager.Find(wantedUser.UserName, password);

					if (user == null)
						return null;

					return userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
				}
			}
		}

		public void DeleteUser(int userId)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var userManager = userManagerFactory.Invoke();
				var acc = userRepository.GetById(userId);
				var user = userManager.FindById(userId);
				userRepository.Delete(acc);
				userManager.Delete(user);
				uow.Commit();
			}
		}

		#endregion

		#region Update

		public void EditUser(AccountDTO account)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				using (var userManager = userManagerFactory.Invoke())
				{
					var acc = userRepository.GetById(account.ID);
					Mapper.Map(account, acc);

					userRepository.Update(acc);

					var user = userManager.Users.Include(a => a.Account).FirstOrDefault(u => u.Email.Equals(acc.User.Email));
					if (user == null) return;

					if (account.Email != null)
						user.Email = account.Email;


					if (account.UserName != null)
						user.UserName = account.UserName;
					if (account.Password != null)
						user.PasswordHash = userManager.PasswordHasher.HashPassword(account.Password);

					userManager.Update(user);

					uow.Commit();
				}
			}
		}

		public void AddUsersToFriends(int id1, int id2)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var user1 = userRepository.GetById(id1);
				var user2 = userRepository.GetById(id2);

				friendshipRepository.Insert(new Friendship(user1, user2));
				uow.Commit();
			}
		}

		public void RemoveUsersFromFriends(AccountDTO accountDto, AccountDTO user2Dto)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var query = GetQuery(new FriendshipFilter {Account = accountDto, Account2 = user2Dto});

				var frship = query.Execute().FirstOrDefault();
				RemoveFriendship(frship);
				uow.Commit();
			}
		}


		public void RemoveFriendship(FriendshipDTO friendship)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				friendshipRepository.Delete(friendshipRepository.GetById(friendship.ID));
				uow.Commit();
			}
		}

		public void AddUsersToFriends(AccountDTO user1Dto, AccountDTO user2Dto)
		{
			AddUsersToFriends(user1Dto.ID, user2Dto.ID);
		}

		#endregion

		#region Get

		public AccountDTO GetUserById(int userId)
		{
			using (UnitOfWorkProvider.Create())
			{
				var user = userRepository.GetById(userId, u => u.User);
				if (user == null) return null;
				var accountDto = Mapper.Map<AccountDTO>(user);
				accountDto.UserName = user.User.UserName;
				accountDto.Email = user.User.Email;

				return accountDto;
			}
		}

		public UserListQueryResultDTO ListUsers(UserFilter filter, int page = 0)
		{
			using (UnitOfWorkProvider.Create())
			{
				var query = GetQuery(filter);
				query.Skip = (page > 0 ? page - 1 : 0)*UserPageSize;
				query.Take = UserPageSize;
				query.AddSortCriteria("Surname");


				return new UserListQueryResultDTO
				{
					RequestedPage = page,
					ResultCount = query.GetTotalRowCount(),
					ResultUsers = query.Execute(),
					Filter = filter
				};
			}
		}

		public List<AccountDTO> ListFriendsOfUser(AccountDTO account, int page = 0)
		{
			using (UnitOfWorkProvider.Create())
			{
				var filter = new FriendshipFilter {Account = account};

				var query = GetQuery(filter);
//				query.Skip = (page > 0 ? page - 1 : 0) * UserPageSize;
//				query.Take = UserPageSize;
//				query.AddSortCriteria("Surname");


				var queryRes = new FriendListQueryResultDTO
				{
					RequestedPage = page,
					ResultCount = query.GetTotalRowCount(),
					ResultFriendships = query.Execute(),
					Filter = filter
				};

				var retList = new List<AccountDTO>();
				foreach (var friendship in queryRes.ResultFriendships)
				{
					Account tmpUs = null;


					if (friendship.User1Id.Equals(account.ID)) tmpUs = userRepository.GetById(friendship.User2Id);
					else if (friendship.User2Id.Equals(account.ID)) tmpUs = userRepository.GetById(friendship.User1Id);

					retList.Add(Mapper.Map<AccountDTO>(tmpUs));
				}
				return retList;
			}
		}

		public List<GroupDTO> ListUsersGroups(AccountDTO account)
		{
			using (UnitOfWorkProvider.Create())
			{
				var userEnt = userRepository.GetById(account.ID, u => u.Groups);
				return Mapper.Map<List<GroupDTO>>(userEnt.Groups);
			}
		}

		#endregion

		#region additional

		private IQuery<AccountDTO> GetQuery(UserFilter filter)
		{
			var query = userListQuery;
			query.ClearSortCriterias();
			query.Filter = filter;
			return query;
		}

		private IQuery<FriendshipDTO> GetQuery(FriendshipFilter filter)
		{
			var query = friendListQuery;
			query.ClearSortCriterias();
			query.Filter = filter;
			return query;
		}

		#endregion
	}
}