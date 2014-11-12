using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SurrealistGames.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Favorites",
                url: "favorites",
                defaults: new { controller = "SavedQuestionGameResult", action = "SavedResults" }
                );

            routes.MapRoute(
                name: "Play",
                url: "play",
                defaults: new {controller = "QuestionGame", action = "Play"});

            routes.MapRoute(
                name: "View",
                url: "view",
                defaults: new {controller = "QuestionGame", action = "Index"});

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
