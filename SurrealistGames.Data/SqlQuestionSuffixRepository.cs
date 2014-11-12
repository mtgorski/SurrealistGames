using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.Utility;

namespace SurrealistGames.Repositories
{
    public class SqlQuestionSuffixRepository : IQuestionSuffixRepository
    {

        private IRandomBehavior _rng;

        public SqlQuestionSuffixRepository(IRandomBehavior rng)
        {
            _rng = rng;
        }


        public QuestionSuffix GetRandom()
        {
            var questionSuffix = new QuestionSuffix();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand()
                {
                    CommandText = "RandomQuestionSuffix_MaxRandomId",
                    Connection = cn,
                    CommandType = CommandType.StoredProcedure
                };
                int maxRandomId;

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    maxRandomId = (int) dr["MaxRandomQuestionSuffixId"];
                }

                bool randomQuestionResultIsNull = true;

                do
                {
                    cmd = new SqlCommand()
                    {
                        CommandText = "QuestionSuffix_GetRandom",
                        Connection = cn,
                        CommandType = CommandType.StoredProcedure
                    };

                    int randomSuffixId = _rng.GetRandom(1, maxRandomId);
                    cmd.Parameters.AddWithValue("@RandomQuestionSuffixId", randomSuffixId);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            questionSuffix.Content = dr["QuestionSuffixContent"].ToString();
                            questionSuffix.QuestionSuffixId = (int)dr["QuestionSuffixId"];
                            randomQuestionResultIsNull = false;
                        }
                        
                    } 
                } while (randomQuestionResultIsNull);

            }

            return questionSuffix;
        }

        public void Save(QuestionSuffix suffix)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand()
                {
                    CommandText = "QuestionSuffix_Insert",
                    Connection = cn,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@QuestionSuffixContent", suffix.Content);

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        suffix.QuestionSuffixId = (int) dr["QuestionSuffixId"];
                    }
                }
            }
        }
    }
}