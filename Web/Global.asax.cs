using Data.Context;
<<<<<<< HEAD
using System.Data.Entity;
=======
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web;

namespace SysIgreja
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
<<<<<<< HEAD
            SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Data.Migrations.Configuration>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
        }
    }
}
