using System.Web.Mvc;
using System.Web.Routing;

namespace ShareMarket.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                  name: "Entity",
                  url: "Entity/{action}/{type}/{id}",
                  defaults: new { controller = "Entity", action = "List", id = UrlParameter.Optional }
              );

            routes.MapRoute(
                  name: "Default",
                  url: "{controller}/{action}/{id}",
                  defaults: new { controller = "Home", action = "Dashboard", id = UrlParameter.Optional }
              );
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "UrlRoute",
               url: "{controller}/{action}/{type}/{cityId}",
               defaults: new { controller = "Home", action = "Dashboard", type = UrlParameter.Optional, cityId = UrlParameter.Optional }
           );
           // routes.MapRoute(
           //    name: "Entity",
           //    url: "Entity/{action}/{type}",
           //    defaults: new { controller = "Entity", action = "Dashboard", type = UrlParameter.Optional }
           //);
           
        }
    }
}