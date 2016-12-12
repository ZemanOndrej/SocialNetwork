using System.Data.Entity;
using BL.AppRigantiInfrastructure;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Identity
{
	public class AppRoleStore : RoleStore<AppRole, int, AppUserRole>
	{
		public AppRoleStore(IUnitOfWorkProvider unitOfWorkProvider)
			: base((unitOfWorkProvider.GetCurrent() as AppUnitOfWork)?.Context)
		{
		}

		public AppRoleStore(DbContext context)
			: base(context)
		{
		}
	}
}