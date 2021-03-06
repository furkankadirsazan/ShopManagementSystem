﻿using System.Web.Mvc;
using System.Web.Routing;

namespace ShopManagementSystem.WebUI
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { area = "Admin", controller = "home", action = "index", id = UrlParameter.Optional }, // Parameter defaults
                null,
                new[] { "ShopManagementSystem.WebUI.Areas.Admin.Controllers" }
            ).DataTokens.Add("area", "Admin");


            //  routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "home", action = "index", id = UrlParameter.Optional }
            //);
        }
    }
}
