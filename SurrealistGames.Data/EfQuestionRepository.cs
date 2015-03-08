using SurrealistGames.Models;
using SurrealistGames.Models.Abstract;
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
        private readonly IEfDbContext _context;

        public EfQuestionRepository(IRandomBehavior rng, IEfDbContext dbContext)
        {
            _rng = rng;
            _context = dbContext;
        }

        public Models.Question GetRandom()
        {

            var maxRandomId = _context.Database.SqlQuery<int>("exec RandomQuestion_MaxRandomId").First();

            var questionId = _rng.GetRandom(1, maxRandomId);

            var questionQuery = _context.Database.SqlQuery<Models.Question>("exec Question_GetRandom @RandomQuestionId",
                new SqlParameter("@RandomQuestionId", questionId));

            return questionQuery.FirstOrDefault();

        }

        public void Save(Models.Question prefix)
        {

            var insertId = _context.Database.SqlQuery<int>("exec Question_Insert @QuestionContent",
                new SqlParameter("@QuestionContent", prefix.QuestionContent));

            prefix.QuestionId = insertId.First();

        }


        public void Disable(int questionId)
        {

            _context.Database.ExecuteSqlCommand("delete from dbo.RandomQuestion where QuestionID = @questionId",
                new SqlParameter("@QuestionID", questionId));

            _context.Database.ExecuteSqlCommand("exec dbo.RandomQuestion_ResetIDsAfterDelete");

        }

        public Models.Question GetById(int questionId)
        {
            return _context.Questions.FirstOrDefault(q => q.QuestionId == questionId);
        }

        public Content GetContentById(int contentId)
        {
            return GetById(contentId);
        }

        public IEnumerable<Content> GetTopReportedAndUnmoderatedContent(int numberOfResults)
        {
            return _context.Questions
                        .Where(q => !q.ApprovingUserId.HasValue && !q.RemovingUserId.HasValue)
                        .OrderByDescending(q => q.Reports.Count)
                        .Take(numberOfResults)
                        .ToList();
        }
    }
}
