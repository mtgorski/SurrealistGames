using System;
namespace SurrealistGames.Data
{
    public interface IEfDbContext
    {
        System.Data.Entity.DbSet<SurrealistGames.Models.Answer> Answers { get; set; }
        System.Data.Entity.DbSet<SurrealistGames.Models.Question> Questions { get; set; }
        System.Data.Entity.DbSet<SurrealistGames.Models.Report> Reports { get; set; }
        System.Data.Entity.DbSet<SurrealistGames.Models.UserInfo> UserInfos { get; set; }
        System.Data.Entity.DbSet<SurrealistGames.Models.SavedQuestionGameResult> UserSavedOutcomes { get; set; }
        int SaveChanges();
    }
}
