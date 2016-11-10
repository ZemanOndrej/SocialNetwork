using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.GroupDTOs;
using BL.Queries;
using BL.Repositories;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Services.Group
{
	public class GroupService : AppService,IGroupService
	{
		public int GroupPageSize => 20;


		#region Dependency
		private readonly GroupRepository groupRepository;

		private readonly GroupListQuery groupListQuery;

		private readonly UserRepository userRepository;

		public GroupService(GroupListQuery groupListQuery, GroupRepository groupRepository, UserRepository userRepository)
		{
			this.groupListQuery = groupListQuery;
			this.groupRepository = groupRepository;
			this.userRepository = userRepository;
		}

		#endregion


		#region CreateDelete
		public int CreateGroup(GroupDTO group)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var groupEnt = Mapper.Map<DAL.Entities.Group>(group);

				groupEnt.DateCreated = DateTime.Now;

				groupRepository.Insert(groupEnt);
				uow.Commit();
				return groupEnt.ID;
			}
		}

		public void DeleteGroup(int id)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				groupRepository.Delete(id);
				uow.Commit();
			}
		}

		public void DeleteGroup(GroupDTO group)
		{
			DeleteGroup(group.ID);
		}


		#endregion

		#region Update

		public void EditGroupName(GroupDTO group)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var groupEnt = groupRepository.GetById(group.ID,g=>g.Accounts,g=>g.GroupPosts);
				groupEnt.Name = group.Name;
				groupRepository.Update(groupEnt);
				uow.Commit();
			}
		}

		public void AddUserToGroup(GroupDTO group, UserDTO user)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var groupEnt = groupRepository.GetById(group.ID);
				var userEnt = userRepository.GetById(user.ID);
				groupEnt.Accounts.Add(userEnt);
				groupRepository.Update(groupEnt);
				uow.Commit();
			}
		}

		public void RemoveUserFromGroup(GroupDTO group, UserDTO user)
		{
			using (var uow = UnitOfWorkProvider.Create())
			{
				var groupEnt = groupRepository.GetById(group.ID);
				var userEnt = userRepository.GetById(user.ID);
				groupEnt.Accounts.Remove(userEnt);
				groupRepository.Update(groupEnt);
				uow.Commit();
			}
		}

		

		#endregion

		#region Get


		public GroupDTO GetGroupById(int id)
		{
			using (UnitOfWorkProvider.Create())
			{
				var group = groupRepository.GetById(id,g=>g.Accounts,g=>g.GroupPosts);
				return group != null ? Mapper.Map<GroupDTO>(group) : null;
			}
		}

		public GroupListQueryResultDTO ListGroups(GroupFilter filter, int page = 0)
		{
			using (UnitOfWorkProvider.Create())
			{
				var query = GetQuery(filter);
				query.Skip = (page > 0 ? page - 1 : 0) * GroupPageSize;
				query.Take = GroupPageSize;

				query.AddSortCriteria(dto => dto.Name);


				return new GroupListQueryResultDTO
				{
					RequestedPage = page,
					ResultCount = query.GetTotalRowCount(),
					ResultGroups = query.Execute(),
					Filter = new GroupFilter()
				};

			}
		}

		public List<UserDTO> ListUsersInGroup(GroupDTO group)
		{
			using (UnitOfWorkProvider.Create())
			{
				var groupEnt = groupRepository.GetById(group.ID, g => g.Accounts);
				return Mapper.Map<List<UserDTO>>(groupEnt.Accounts);
			}
		}





		#endregion

		#region additional

		private IQuery<GroupDTO> GetQuery(GroupFilter filter)
		{
			var query = groupListQuery;
			query.ClearSortCriterias();
			query.Filter = filter;
			return query;
		}

		#endregion



	}
}
