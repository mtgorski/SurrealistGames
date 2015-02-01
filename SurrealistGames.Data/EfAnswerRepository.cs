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
    public class EfAnswerRepository : IAnswerRepository
    {
        private readonly IRandomBehavior _rng;

        public EfAnswerRepository(IRandomBehavior rng)
        {
            _rng = rng;
        }

        public Models.Answer GetRandom()
        {
            using (var db = new EfDbContext())
            {
                var maxRandomId = db.Database.SqlQuery<int>("exec RandomAnswer_MaxRandomId").First();

                var questionId = _rng.GetRandom(1, maxRandomId);

                var questionQuery = db.Database.SqlQuery<Models.Answer>("exec Answer_GetRandom @RandomAnswerId",
                    new SqlParameter("@RandomAnswerId", questionId));

                return questionQuery.FirstOrDefault();
            }
        }

        public void Save(Models.Answer prefix)
        {
            using (var db = new EfDbContext())
            {
                var insertId = db.Database.SqlQuery<int>("exec Answer_Insert @AnswerContent",
                    new SqlParameter("@AnswerContent", prefix.AnswerContent));

                prefix.AnswerId = insertId.First();
            }
        }


        public void Disable(int answerId)
        {
            throw new NotImplementedException();
        }
    }
}
