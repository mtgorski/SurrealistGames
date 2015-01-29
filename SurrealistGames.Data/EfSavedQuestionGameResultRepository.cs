using SurrealistGames.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Data
{
    public class EfSavedQuestionGameResultRepository : ISavedQuestionGameResultRepo
    {

        public void Save(Models.SavedQuestionGameResult item)
        {
            using (var db = new EfDbContext())
            {
                 db.Database.ExecuteSqlCommand("exec SavedQuestion_Insert @QuestionId, @AnswerId, @UserInfoId",
                    new SqlParameter("@QuestionId", item.QuestionId),
                    new SqlParameter("@AnswerId", item.AnswerId),
                    new SqlParameter("@UserInfoId", item.UserInfoId));
            }
        }

        public async Task<List<Models.UserSavedOutcomeView>> GetAllSavedOutcomesByUserId(int userInfoId)
        {
            using(var db = new EfDbContext())
            {
                var query = db.UserSavedOutcomes.Where(x => x.UserInfoId == userInfoId)
                                .Select(x => new Models.UserSavedOutcomeView
                                {
                                    Question = x.Question.QuestionContent,
                                    Answer = x.Answer.AnswerContent,
                                    SavedQuestionId = x.SavedQuestionGameResultId
                                });

                return await query.ToListAsync();
            }
        }

        public bool UserOwnsSavedResult(int userInfoId, int savedQuestionId)
        {
            using( var db = new EfDbContext())
            {
                var savedGame = db.UserSavedOutcomes.FirstOrDefault(
                    so => so.SavedQuestionGameResultId == savedQuestionId);

                return savedGame != null && savedGame.UserInfoId == userInfoId;
            }
        }

        public void Delete(int savedQuestionId)
        {
            using(var db = new EfDbContext())
            {
                var toRemove = db.UserSavedOutcomes.Where(m => m.SavedQuestionGameResultId == savedQuestionId).FirstOrDefault();
                db.UserSavedOutcomes.Remove(toRemove);

                db.SaveChanges();
            }
        }
    }
}
