using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.Repositories;

namespace SurrealistGames.Data
{
    public class SqlSavedQuestionGameResultRepo : ISavedQuestionGameResultRepo
    {
        public void Save(Models.SavedQuestionGameResult item)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand()
                {
                    CommandText = "SavedQuestion_Insert",
                    CommandType = CommandType.StoredProcedure,
                    Connection = cn
                };

                cmd.Parameters.AddWithValue("@QuestionPrefixId", item.QuestionPrefixId);
                cmd.Parameters.AddWithValue("@QuestionSuffixId", item.QuestionSuffixId);
                cmd.Parameters.AddWithValue("@UserInfoId", item.UserInfoId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }


        public List<UserSavedOutcomeView> GetAllSavedOutcomesByUserId(int userInfoid)
        {
            using(var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var p = new DynamicParameters();
                p.Add("@UserInfoId", userInfoid);

                return cn.Query<UserSavedOutcomeView>("UserInfo_GetSavedQuestions",
                    p, commandType: CommandType.StoredProcedure).ToList();
            }
        }


        public bool UserOwnsSavedResult(int userInfoId, int savedQuestionId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                return cn.ExecuteScalar<bool>("select dbo.UserOwnsResult(@UserInfoId, @SavedQuestionId)",
                           new { @UserInfoId = userInfoId, @SavedQuestionId = savedQuestionId} );    
            }
        }

        public void Delete(int savedQuestionId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "SavedQuestionGameResult_Delete",
                    Connection = cn
                };

                cmd.Parameters.AddWithValue("@SavedQuestionId", savedQuestionId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
