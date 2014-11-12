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
            throw new NotImplementedException();
        }
    }
}
