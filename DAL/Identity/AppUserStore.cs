using System.Data.Entity;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Identity
{
	public class AppUserStore : UserStore<User, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
	{
		public AppUserStore(DbContext context)
			: base(context)
		{
		}
	}
}