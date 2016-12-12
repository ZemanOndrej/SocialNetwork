using System.Data.Entity;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Identity
{
	public class AppRoleStore : RoleStore<AppRole, int, AppUserRole>
	{
		public AppRoleStore(DbContext context)
			: base(context)
		{
		}
	}
}