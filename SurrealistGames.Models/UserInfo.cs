using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models
{
    public class UserInfo
    {
        public int UserInfoId { get; set; }
        /// <summary>
        /// AspNetUsers Id
        /// </summary>
        public string Id { get; set; }

        public virtual ICollection<UserSavedOutcomeView> SavedGames { get; set; }
    }
}
