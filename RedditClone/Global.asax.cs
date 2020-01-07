using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RedditClone
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           
            //Database.SetInitializer<Models.ApplicationDbContext>(null);
            Database.SetInitializer<Models.ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<Models.ApplicationDbContext>());
            //Database.SetInitializer<Models.ApplicationDbContext>(new DropCreateDatabaseAlways<Models.ApplicationDbContext>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.ApplicationDbContext, RedditClone.Migrations.Configuration>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
