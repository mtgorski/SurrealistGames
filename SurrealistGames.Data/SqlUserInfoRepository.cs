using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurrealistGames.Models;
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
            var result = new UserInfo();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "UserInfo_GetByAspId",
                    Connection = cn
                };

                cmd.Parameters.AddWithValue("@AspId", aspNetId);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result.UserInfoId = (int) dr["UserInfoId"];
                        result.Id = dr["Id"].ToString();
                    }
                    else
                    {
                        result = null;
                    }
                }

            }

            return result;
        }
    }
}
