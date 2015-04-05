using AutoMapper;
using SurrealistGames.Models;
using SurrealistGames.Models.Abstract;
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
        private readonly IEfDbContext _context;
        private readonly IMappingEngine _mapper;

        public EfAnswerRepository(IRandomBehavior rng, IEfDbContext context, IMappingEngine mapper)
        {
            _rng = rng;
            _context = context;
            _mapper = mapper;
        }

        public Models.Answer GetRandom()
        {
            var maxRandomId = _context.Database.SqlQuery<int>("exec RandomAnswer_MaxRandomId").First();

            var questionId = _rng.GetRandom(1, maxRandomId);
            
            var questionQuery = _context.Database.SqlQuery<Models.Answer>("exec Answer_GetRandom @RandomAnswerId",
                new SqlParameter("@RandomAnswerId", questionId));

            return questionQuery.FirstOrDefault();
        }

        public void Save(Models.Answer prefix)
        {
             var insertId = _context.Database.SqlQuery<int>("exec Answer_Insert @AnswerContent",
                    new SqlParameter("@AnswerContent", prefix.AnswerContent));

             prefix.AnswerId = insertId.First();
        }


        public void Disable(int answerId)
        {
            _context.Database.ExecuteSqlCommand("delete from dbo.RandomAnswer where AnswerID = @answerId",
                new SqlParameter("@answerId", answerId));

            _context.Database.ExecuteSqlCommand("exec dbo.RandomAnswer_ResetIDsAfterDelete");
        }

        public Answer GetById(int answerId)
        {
            return _context.Answers.FirstOrDefault(a => a.AnswerId == answerId);
        }

        public Content GetContentById(int contentId)
        {
            return GetById(contentId);
        }

        public IEnumerable<Content> GetTopReportedAndUnmoderatedContent(int numberOfAnswers)
        {
            return _context.Answers
                        .Where(
                            a => !a.ApprovingUserId.HasValue 
                                    && !a.RemovingUserId.HasValue
                                    && a.Reports.Count > 0)
                        .OrderByDescending(a => a.Reports.Count)
                        .Take(numberOfAnswers)
                        .ToList();
        }


        public void Remove(RemoveContentRequest request)
        {
            var answer = _context.Answers.First(r => r.AnswerId == request.AnswerId);
            answer.RemovedOn = DateTime.UtcNow;
            answer.RemovingUserId = request.RequestingUserId;
            _context.SaveChanges();
        }

        public void Update(Content content)
        {
            var updated = content as Answer;
            var current = _context.Answers.FirstOrDefault(a => a.AnswerId == updated.AnswerId);
            _mapper.Map<Answer, Answer>(updated, current);
            _context.SaveChanges();
        }

        public void AddToOutcomes(Content content)
        {
            var answer = content as Answer;
            var isAlreadyAdded = _context.Database.SqlQuery<int>(
                "select count(*) from dbo.RandomAnswer where AnswerId = @AnswerId",
                new SqlParameter("@AnswerId", answer.AnswerId)
                ).First() > 0;

            if(!isAlreadyAdded)
            {
                _context.Database.ExecuteSqlCommand(
                    "insert into RandomAnswer(AnswerID, RandomAnswerID) values(@AnswerId, (select Max(RandomAnswerID) + 1 from RandomAnswer ));",
                    new SqlParameter("AnswerId", answer.AnswerId)
                    );
            }
        }
    }
}
