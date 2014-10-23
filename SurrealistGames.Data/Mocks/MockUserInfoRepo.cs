using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;

namespace SurrealistGames.Data.Mocks
{
    public class MockUserInfoRepo : IUserInfoRepo
    {
        private static List<UserInfo> _allItems = new List<UserInfo>();

        public void Add(UserInfo item)
        {
            _allItems.Add(item);
        }
    }
}
