using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using WebEssentials.AspNetCore.Pwa;

[assembly: OwinStartupAttribute(typeof(SysIgreja.Startup))]
namespace SysIgreja
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Your other services
            services.AddMvc();
<<<<<<< HEAD
            services.AddProgressiveWebApp(new PwaOptions
            {
=======
            services.AddProgressiveWebApp(new PwaOptions {
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                RegisterServiceWorker = false,
                RegisterWebmanifest = false
            });
        }
    }
}
