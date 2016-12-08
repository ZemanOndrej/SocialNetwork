using DAL.Entities.Identity;
using Microsoft.AspNet.Identity;

namespace DAL.Identity
{
	public class AppUserManager : UserManager<User, int>
	{
		public AppUserManager(IUserStore<User, int> store) : base(store)
		{
		}
	}
}