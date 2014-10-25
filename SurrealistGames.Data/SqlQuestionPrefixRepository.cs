using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.Models;
using SurrealistGames.Utility;

namespace SurrealistGames.Repositories
{
    public class SqlQuestionPrefixRepository : IQuestionPrefixRepository
    {
        private IRandomBehavior _rng;

        public SqlQuestionPrefixRepository(IRandomBehavior rng)
        {
            _rng = rng;
        }

        public QuestionPrefix GetRandom()
        {
            var result = new QuestionPrefix();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand();
                int maxRandomId;
                cmd.CommandText = "RandomQuestionPrefix_MaxRandomId";
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    maxRandomId = (int)dr["MaxRandomQuestionPrefixId"];
                }

                bool questionResultSetIsEmpty = true;

                do
                {
                    int randomPrefixId = _rng.GetRandom(1, maxRandomId);

                    cmd = new SqlCommand();
                    cmd.CommandText = "QuestionPrefix_GetRandom";
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RandomQuestionPrefixId", randomPrefixId);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            questionResultSetIsEmpty = false;
                        }
                        result.Content = dr["QuestionPrefixContent"].ToString();
                        result.QuestionPrefixId = (int)dr["QuestionPrefixId"];
                    } 
                } while (questionResultSetIsEmpty);
            }

            return result;
        }

        public void Save(QuestionPrefix prefix)
        {
            using (
                var cn =
                    new SqlConnection(
                        Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "QuestionPrefix_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QuestionPrefixContent", prefix.Content);
                cmd.Connection = cn;

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}