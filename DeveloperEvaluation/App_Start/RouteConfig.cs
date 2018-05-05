using System.Web.Mvc;
using System.Web.Routing;

namespace DeveloperEvaluation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{NumbersWithComma}",
                defaults: new { controller = "Home", action = "Index", NumbersWithComma = UrlParameter.Optional }
            );
        }
    }
}
