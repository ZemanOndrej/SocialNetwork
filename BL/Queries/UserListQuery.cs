using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BL.AppRigantiInfrastructure;
using BL.DTO;
using BL.DTO.Filters;
using BL.DTO.UserDTOs;
using Castle.Core.Internal;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
	public class UserListQuery : AppQuery<AccountDTO>
	{

		
		public UserFilter Filter { get; set; }


		public UserListQuery(IUnitOfWorkProvider provider) : base(provider){}

		protected override IQueryable<AccountDTO> GetQueryable()
		{

			IQueryable<Account> query = Context.Accounts;



			if (!string.IsNullOrEmpty(Filter.Name))
			{
				query = query.Where(u => u.Name.ToLower()
					.Contains(Filter.Name.ToLower()));
			}

			if (!string.IsNullOrEmpty(Filter.Surname))
			{
				query = query.Where(u => u.Surname.ToLower()
					.Contains(Filter.Surname.ToLower()));
			}

			if (!string.IsNullOrEmpty(Filter.Login))
			{
				query = query.Where(u => u.User.UserName.Equals(Filter.Login) );
			}

			if (!string.IsNullOrEmpty(Filter.Email))
			{


				query = query.Where(u => u.User.Email.Equals(Filter.Email) );
			}

			if (Filter.Gender != null)
			{
				query = query.Where(u => u.Gender==Filter.Gender);
			}
			if (Filter.Fullname != null)
			{
				query = query.Where(u => (u.Name +" "+ u.Surname).Contains(Filter.Fullname));
			}

			if (Filter.Group != null)
			{
				var group = Mapper.Map<Group>(Filter.Group);
				query = query.Where(u => u.Groups.Contains(new Group {ID = group.ID}));
			}

			return query.ProjectTo<AccountDTO>();
		}


	}
}
