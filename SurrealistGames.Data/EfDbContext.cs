using SurrealistGames.Models;
using SurrealistGames.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Data
{
    public class EfDbContext : DbContext
    {
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<SavedQuestionGameResult> UserSavedOutcomes { get; set; }
        public DbSet<QuestionPrefix> QuestionPrefixes { get; set; }
        public DbSet<QuestionSuffix> QuestionSuffixes { get; set; }

        public EfDbContext() : base(Settings.GetConnectionString())
        {
            Database.SetInitializer<EfDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SavedQuestionGameResult>()
                .ToTable("dbo.SavedQuestionGameResult")
                .Property(e => e.SavedQuestionGameResultId).HasColumnName("SavedQuestionId");

            modelBuilder.Ignore<UserSavedOutcomeView>();


            modelBuilder.Entity<UserInfo>().ToTable("dbo.UserInfo");

            modelBuilder.Entity<QuestionPrefix>().ToTable("dbo.QuestionPrefix");

            modelBuilder.Entity<QuestionSuffix>().ToTable("dbo.QuestionSuffix");
        }
    }
}
