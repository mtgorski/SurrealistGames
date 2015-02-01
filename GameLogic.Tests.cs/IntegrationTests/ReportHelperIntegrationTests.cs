using NUnit.Framework;
using SurrealistGames.GameLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SurrealistGames.Models;

namespace GameLogic.Tests.cs.IntegrationTests
{
    [TestFixture]
    public class ReportHelperIntegrationTests
    {
        public ReportHelperTests UnitImplementation { get; set; }
        public ReportHelper Target { get; set; } 

        [SetUp]
        public void SetUp()
        {
            UnitImplementation = new ReportHelperTests();
            UnitImplementation.SetUp();

            Mapper.CreateMap<ReportRequest, Report>();

            Target = new ReportHelper(AutoMapper.Mapper.Engine,
                UnitImplementation.ReportRepositoryMock.Object, UnitImplementation.ConfigurationMock.Object,
                UnitImplementation.QuestionRepositoryMock.Object, UnitImplementation.AnswerRepositoryMock.Object);
        }

        [Test]
        [Ignore]
        public void Exploratory()
        {
            UnitImplementation.GivenReportRequest.UserInfoId = 10;
            UnitImplementation.GivenReportRequest.QuestionId = 99;

            var response = Target.MakeReport(UnitImplementation.GivenReportRequest);
        }
    }
}
