using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities.Identity
{
	public class User : IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>
	{
		[Required]
		public virtual Account Account { get; set; }
	}

	public class AppUserClaim : IdentityUserClaim<int>
	{
	}

	public class AppUserLogin : IdentityUserLogin<int>
	{
	}

	public class AppUserRole : IdentityUserRole<int>
	{
		public string Code { get; set; }
	}

	public class AppRole : IdentityRole<int, AppUserRole>
	{
	}
}