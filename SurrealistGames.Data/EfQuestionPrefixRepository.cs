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
    public class EfQuestionRepository : IQuestionRepository
    {
        private readonly IRandomBehavior _rng;

        public EfQuestionRepository(IRandomBehavior rng)
        {
            _rng = rng;
        }

        public Models.Question GetRandom()
        {
            using(var db = new EfDbContext())
            {
                var maxRandomId = db.Database.SqlQuery<int>("exec RandomQuestion_MaxRandomId").First();

                var questionId = _rng.GetRandom(1, maxRandomId);

                var questionQuery = db.Database.SqlQuery<Models.Question>("exec Question_GetRandom @RandomQuestionId",
                    new SqlParameter("@RandomQuestionId", questionId));

                return questionQuery.FirstOrDefault();
            }
        }

        public void Save(Models.Question prefix)
        {
            using(var db = new EfDbContext())
            {
                var insertId = db.Database.SqlQuery<int>("exec Question_Insert @QuestionContent",
                    new SqlParameter("@QuestionContent", prefix.QuestionContent));

                prefix.QuestionId = insertId.First();
            }
        }
    }
}
