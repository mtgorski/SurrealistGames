using SurrealistGames.Models.Interfaces;
using SurrealistGames.Repositories;
using SurrealistGames.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Data
{
    public class EfQuestionPrefixRepository : IQuestionPrefixRepository
    {
        private readonly IRandomBehavior _rng;

        public EfQuestionPrefixRepository(IRandomBehavior rng)
        {
            _rng = rng;
        }

        public Models.QuestionPrefix GetRandom()
        {
            using(var db = new EfDbContext())
            {
                var maxRandomId = db.Database.SqlQuery<int>("exec RandomQuestionPrefix_MaxRandomId").First();

                var questionId = _rng.GetRandom(1, maxRandomId);

                var questionQuery = db.Database.SqlQuery<Models.QuestionPrefix>("exec QuestionPrefix_GetRandom @RandomQuestionPrefixId",
                    new SqlParameter("@RandomQuestionPrefixId", questionId));

                return questionQuery.FirstOrDefault();
            }
        }

        public void Save(Models.QuestionPrefix prefix)
        {
            using(var db = new EfDbContext())
            {
                var insertId = db.Database.SqlQuery<int>("exec QuestionPrefix_Insert @QuestionPrefixContent",
                    new SqlParameter("@QuestionPrefixContent", prefix.QuestionPrefixContent));

                prefix.QuestionPrefixId = insertId.First();
            }
        }
    }
}
