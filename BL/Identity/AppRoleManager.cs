using DAL.Entities.Identity;
using Microsoft.AspNet.Identity;

namespace BL.Identity
{
	public class AppRoleManager : RoleManager<AppRole, int>
	{
		public AppRoleManager(IRoleStore<AppRole, int> store) : base(store)
		{
		}
	}
}