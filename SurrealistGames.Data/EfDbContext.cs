using SurrealistGames.Models;
using SurrealistGames.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Data
{
    public class EfDbContext : DbContext, SurrealistGames.Data.IEfDbContext
    {
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<SavedQuestionGameResult> UserSavedOutcomes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Report> Reports { get; set; }

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

            modelBuilder.Entity<Question>().ToTable("dbo.Question")
                .Ignore(e => e.Id)
                .Ignore(e => e.Value)
                .Ignore(e => e.IsApproved);

            modelBuilder.Entity<Answer>().ToTable("dbo.Answer")
                .Ignore(e => e.Id)
                .Ignore(e => e.Value)
                .Ignore(e => e.IsApproved);

            modelBuilder.Entity<Report>().ToTable("dbo.Report");
        }
    }
}
