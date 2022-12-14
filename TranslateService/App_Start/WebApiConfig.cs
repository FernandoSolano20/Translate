using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TranslateService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { action = "builtin", id = RouteParameter.Optional },
                constraints: new { id = @"\d+" });

            config.Routes.MapHttpRoute(
                name: "CustomActionApi",
                routeTemplate: "api/{controller}/{action}");
        }
    }
}
