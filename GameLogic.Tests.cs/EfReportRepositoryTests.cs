using Moq;
using NUnit.Framework;
using SurrealistGames.Data;
using SurrealistGames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Testing;
using System.Data.Entity;

namespace GameLogic.Tests.cs
{
    [TestFixture]
    public class EfReportRepositoryTests
    {
        public Mock<IEfDbContext> ContextMock { get; set; }
        public Mock<DbSet<Report>> ReportsMock { get; set; }
        public EfReportRepository Target { get; set; }
        public List<Report> SeedReports { get; set; }

        public Report GivenReport { get; set; }

        [SetUp]
        public void SetUp()
        {
                ContextMock = new Mock<IEfDbContext>();
                GivenReport = new Report();
                ReportsMock = new Mock<DbSet<Report>>();

                Target = new EfReportRepository(ContextMock.Object);

                ContextMock.Setup(m => m.Reports).Returns(ReportsMock.Object);
        }

        [Test]
        public void GivenReport_WhenSave_AddAndSaveChangesToContext()
        {
            MockDbSetExtensions.SetupData(ReportsMock, new List<Report>());
            
            Target.Save(GivenReport);

            ReportsMock.Verify(m => m.Add(It.Is<Report>(r => r == GivenReport)));
            ContextMock.Verify(m => m.SaveChanges());
        }

    }
}
