using SurrealistGames.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                var command = db.Database.ExecuteSqlCommand("exec SavedQuestion_Insert @QuestionPrefixId, @QuestionSuffixId, @UserInfoId",
                    new SqlParameter("@QuestionPrefixId", item.QuestionPrefixId),
                    new SqlParameter("@QuestionSuffixId", item.QuestionSuffixId),
                    new SqlParameter("@UserInfoId", item.UserInfoId));
            }
        }

        public List<Models.UserSavedOutcomeView> GetAllSavedOutcomesByUserId(int userInfoId)
        {
            using(var db = new EfDbContext())
            {
                var query = db.UserSavedOutcomes.Where(x => x.UserInfoId == userInfoId)
                                .Select(x => new Models.UserSavedOutcomeView
                                {
                                    Question = x.QuestionPrefix.QuestionPrefixContent,
                                    Answer = x.QuestionSuffix.QuestionSuffixContent,
                                    SavedQuestionId = x.SavedQuestionGameResultId
                                });

                return query.ToList();
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
