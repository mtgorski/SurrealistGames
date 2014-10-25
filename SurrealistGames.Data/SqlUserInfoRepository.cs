using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.Repositories;

namespace SurrealistGames.Data
{
    public class SqlUserInfoRepository : IUserInfoRepo
    {
        public void Add(Models.UserInfo item)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand()
                {
                    CommandText = "UserInfo_Insert",
                    Connection = cn,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
        }


        public Models.UserInfo GetByAspId(string aspNetId)
        {
            throw new NotImplementedException();
        }
    }
}
