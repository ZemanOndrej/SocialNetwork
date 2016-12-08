using System.Data.Entity;
using BL.AppRigantiInfrastructure;
using DAL;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Identity
{
	public class AppUserStore : UserStore<User, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
	{
		public AppUserStore(IUnitOfWorkProvider unitOfWorkProvider)
		   : base((unitOfWorkProvider.GetCurrent() as AppUnitOfWork)?.Context)
		{
		}

		public AppUserStore(DbContext context)
		   : base(context)
		{
		}

	}
}