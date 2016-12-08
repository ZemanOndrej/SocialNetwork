using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace PL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
			ConfigureAuth(app);

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Account/Login")
			});
		}
    }
}
