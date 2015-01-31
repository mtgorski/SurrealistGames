using AutoMapper;
using Moq;
using NUnit.Framework;
using SurrealistGames.GameLogic.Helpers;
using SurrealistGames.GameLogic.Utility;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Tests.cs
{
    [TestFixture]
    public class ReportHelperTests
    {
        public Mock<IConfig> ConfigurationMock{ get; set; }
        public Mock<IMappingEngine> MappingEngineMock { get; set; }
        public Mock<IReportRepository> ReportRepositoryMock { get; set; }
        public Mock<IAnswerRepository> AnswerRepositoryMock { get; set; }
        public Mock<IQuestionRepository> QuestionRepositoryMock { get; set; }

        public ReportRequest GivenReportRequest { get; set; }
        public Report ExpectedReport { get; set; }

        public ReportHelper Target { get; set; }

        [SetUp]
        public void SetUp()
        {
            GivenReportRequest = new ReportRequest();
            ExpectedReport = new Report();

            MappingEngineMock = new Mock<IMappingEngine>();
            MappingEngineMock.Setup(
                m => m.Map<ReportRequest, Report>(
                    It.Is<ReportRequest>(r => r == GivenReportRequest)))
                    .Returns(ExpectedReport);

            ReportRepositoryMock = new Mock<IReportRepository>();
            AnswerRepositoryMock = new Mock<IAnswerRepository>();
            QuestionRepositoryMock = new Mock<IQuestionRepository>();

            ConfigurationMock = new Mock<IConfig>();

            Target = new ReportHelper(MappingEngineMock.Object,
                                       ReportRepositoryMock.Object, 
                                       ConfigurationMock.Object,
                                       QuestionRepositoryMock.Object,
                                       AnswerRepositoryMock.Object);
        }

        private void SetUpPreexistingReportsOnQuestion(int questionId, int numberOfReports)
        {
            var reportsOnGivenQuestion = new List<Report>();

            for (int i = 0; i < numberOfReports; i++)
            {
                reportsOnGivenQuestion.Add(new Report());
            }

            ReportRepositoryMock.Setup(m => m.GetReportsByQuestionId(questionId))
                    .Returns(reportsOnGivenQuestion);

            ReportRepositoryMock.Setup(m => m.Save(It.Is<Report>(r => r == ExpectedReport)))
                .Callback(() => reportsOnGivenQuestion.Add(ExpectedReport));
        }

        private void SetUpPreexistingReportsOnAnswer(int answerId, int numberOfReports)
        {
            var reportsOnGivenAnswer = new List<Report>();

            for (int i = 0; i < numberOfReports; i++)
            {
                reportsOnGivenAnswer.Add(new Report());
            }

            ReportRepositoryMock.Setup(m => m.GetReportsByAnswerId(answerId))
                    .Returns(reportsOnGivenAnswer);

            ReportRepositoryMock.Setup(m => m.Save(It.Is<Report>(r => r == ExpectedReport)))
                .Callback(() => reportsOnGivenAnswer.Add(ExpectedReport));
        }

        [Test]
        public void GivenReportRequest_WhenMakeReport_PassesCreatedReportToRepository()
        {
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(5);
            GivenReportRequest.AnswerId = 5;
            SetUpPreexistingReportsOnAnswer(answerId: 5, numberOfReports: 2);

            Target.MakeReport(GivenReportRequest);

            ReportRepositoryMock.Verify(m => m.Save(It.Is<Report>(r => r == ExpectedReport)), 
                Times.Once);
        }

        [Test]
        public void GivenReportRequestForQuestionWithOneFewerThanMaxReports_WhenMakeReport_DisableQuestion()
        {
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(5);

            GivenReportRequest.QuestionId = 10;

            SetUpPreexistingReportsOnQuestion(questionId:10, numberOfReports:4);

            QuestionRepositoryMock.Setup(m => m.Disable(It.IsAny<int>()));

            Target.MakeReport(GivenReportRequest);

            QuestionRepositoryMock.Verify(m => m.Disable(It.Is<int>(i => i == 10)), Times.Once);
        }

        [Test]
        public void GivenReportRequestForQuestionWithTwoFewerThanMaxReports_WhenMakeReport_DoNotDisableQuestion()
        {
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(5);

            GivenReportRequest.QuestionId = 10;

            SetUpPreexistingReportsOnQuestion(questionId: 10, numberOfReports: 3);

            QuestionRepositoryMock.Setup(m => m.Disable(It.IsAny<int>()));

            Target.MakeReport(GivenReportRequest);

            QuestionRepositoryMock.Verify(m => m.Disable(It.Is<int>(i => i == 10)), Times.Never);
        }

        [Test]
        public void GivenReportRequestForAnswerWithOneFewerThanMaxReports_WhenMakeReport_DisableAnswer()
        {
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(5);

            GivenReportRequest.AnswerId = 10;

            SetUpPreexistingReportsOnAnswer(answerId: 10, numberOfReports: 4);

            AnswerRepositoryMock.Setup(m => m.Disable(It.IsAny<int>()));

            Target.MakeReport(GivenReportRequest);

            AnswerRepositoryMock.Verify(m => m.Disable(It.Is<int>(i => i == 10)), Times.Once);
        }

        [Test]
        public void GivenReportRequestForAnswerWithTwoFewerThanMaxReports_WhenMakeReport_DoNotDisableAnswer()
        {
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(5);

            GivenReportRequest.AnswerId = 10;

            SetUpPreexistingReportsOnAnswer(answerId: 10, numberOfReports: 3);

            AnswerRepositoryMock.Setup(m => m.Disable(It.IsAny<int>()));

            Target.MakeReport(GivenReportRequest);

            AnswerRepositoryMock.Verify(m => m.Disable(It.Is<int>(i => i == 10)), Times.Never);
        }

        [Test]
        public void GivenReportRequestOnQuestion_WhenMakeReport_ReturnResultingNumberOfReports()
        {
            GivenReportRequest.QuestionId = 10;
            SetUpPreexistingReportsOnQuestion(questionId: 10, numberOfReports: 7);

            var actualResponse = Target.MakeReport(GivenReportRequest);

            Assert.AreEqual(8, actualResponse.NumberOfReports);
        }

        [Test]
        public void GivenReportRequestOnQuestion_WhenMakeReport_ReturnNumberOfReportsRequiredToDisable()
        {
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(18);
            GivenReportRequest.QuestionId = 10;
            SetUpPreexistingReportsOnQuestion(questionId: 10, numberOfReports: 7);

            var actualResponse = Target.MakeReport(GivenReportRequest);

            Assert.AreEqual(18, actualResponse.ReportsDisabledOn);
        }
    }
}
