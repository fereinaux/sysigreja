using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace SysIgreja
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                ExpireTimeSpan = new System.TimeSpan(90, 0, 0, 0),
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/login")
            });
<<<<<<< HEAD
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
=======
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);          
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        }
    }
}