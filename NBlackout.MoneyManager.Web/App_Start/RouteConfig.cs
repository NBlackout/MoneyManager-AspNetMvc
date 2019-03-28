using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace NBlackout.MoneyManager.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            if (routes == null)
                throw new ArgumentNullException(nameof(routes));

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "Main", action = "Index" }
            );
        }
    }
}
