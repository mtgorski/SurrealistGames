using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using SurrealistGames.WebUI.Interfaces;

namespace SurrealistGames.WebUI.Utility
{
    public class UserUtility : IUserUtility
    {
        public bool IsLoggedIn(System.Web.Mvc.Controller controller)
        {
            return controller.User.Identity.IsAuthenticated;
        }


        public string GetAspId(System.Web.Mvc.Controller controller)
        {
            return controller.User.Identity.GetUserId();
        }
    }
}