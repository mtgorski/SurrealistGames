using SurrealistGames.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Data
{
    public class EfUserInfoRepository : IUserInfoRepo
    {
        public void Add(Models.UserInfo item)
        {
            using(var db = new EfDbContext())
            {
                db.UserInfos.Add(item);
                db.SaveChanges();
            }
        }

        public Models.UserInfo GetByAspId(string aspNetId)
        {
            using(var db = new EfDbContext())
            {
                return db.UserInfos.FirstOrDefault(u => u.Id == aspNetId);
            }
        }
    }
}
