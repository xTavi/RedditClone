using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RedditClone
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Communities",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Community", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "Posts",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "CoPost", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
