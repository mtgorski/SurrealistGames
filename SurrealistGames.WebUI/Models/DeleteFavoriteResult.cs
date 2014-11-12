using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurrealistGames.WebUI.Models
{
    public class DeleteFavoriteResult
    {
        public bool IsUserLoggedIn { get; set; }
        public bool IsResultOwnedByUser { get; set; }
    }
}