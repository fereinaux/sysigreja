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
            services.AddProgressiveWebApp(new PwaOptions
            {
                RegisterServiceWorker = false,
                RegisterWebmanifest = false
            });
        }
    }
}
