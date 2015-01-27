using SurrealistGames.Models.Interfaces;
using SurrealistGames.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Data
{
    public class EfQuestionSuffixRepository : IQuestionSuffixRepository
    {
        private readonly IRandomBehavior _rng;

        public EfQuestionSuffixRepository(IRandomBehavior rng)
        {
            _rng = rng;
        }

        public Models.QuestionSuffix GetRandom()
        {
            using (var db = new EfDbContext())
            {
                var maxRandomId = db.Database.SqlQuery<int>("exec RandomQuestionSuffix_MaxRandomId").First();

                var questionId = _rng.GetRandom(1, maxRandomId);

                var questionQuery = db.Database.SqlQuery<Models.QuestionSuffix>("exec QuestionSuffix_GetRandom @RandomQuestionSuffixId",
                    new SqlParameter("@RandomQuestionSuffixId", questionId));

                return questionQuery.FirstOrDefault();
            }
        }

        public void Save(Models.QuestionSuffix prefix)
        {
            using (var db = new EfDbContext())
            {
                var insertId = db.Database.SqlQuery<int>("exec QuestionSuffix_Insert @QuestionSuffixContent",
                    new SqlParameter("@QuestionSuffixContent", prefix.QuestionSuffixContent));

                prefix.QuestionSuffixId = insertId.First();
            }
        }
    }
}
