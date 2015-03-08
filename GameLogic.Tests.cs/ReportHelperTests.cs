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

        public Question ReportedQuestion { get; set; }
        public Answer ReportedAnswer { get; set; }

        public ReportHelper Target { get; set; }

        [SetUp]
        public void SetUp()
        {
            GivenReportRequest = new ReportRequest();
            ExpectedReport = new Report()
            {
                UserInfoId = GivenReportRequest.UserInfoId
            };

            ReportedQuestion = new Question();
            ReportedAnswer = new Answer();

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

        #region MakeReport Setup Methods
        private void SetupModeratorApprovedQuestion()
        {
            ReportedQuestion.ApprovingUserId = 20;
        }

        private void SetupModeratorApprovedAnswer()
        {
            ReportedAnswer.ApprovingUserId = 20;
        }

        private void SetUpPreexistingReportsOnQuestion(int questionId, int numberOfReports)
        {
            SetUpPreexistingReportsOnQuestion(questionId, numberOfReports, Enumerable.Repeat(-1, numberOfReports).ToList());
        }

        private void SetUpPreexistingReportsOnQuestion(int questionId, int numberOfReports, List<int> reportingUserIds)
        {
            var reportsOnGivenQuestion = new List<Report>();

            for (int i = 0; i < numberOfReports; i++)
            {
                reportsOnGivenQuestion.Add(new Report() { UserInfoId = reportingUserIds[i] });
            }

            ReportRepositoryMock.Setup(m => m.GetReportsByQuestionId(questionId))
                    .Returns(reportsOnGivenQuestion);

            ReportRepositoryMock.Setup(m => m.Save(It.Is<Report>(r => r == ExpectedReport)))
                .Callback(() => reportsOnGivenQuestion.Add(ExpectedReport));

            QuestionRepositoryMock.Setup(m => m.GetById(It.Is<int>(id => id == GivenReportRequest.QuestionId.Value)))
                .Returns(ReportedQuestion);

            ReportedQuestion.QuestionId = GivenReportRequest.QuestionId.Value;
        }

        private void SetUpPreexistingReportsOnAnswer(int answerId, int numberOfReports)
        {
            SetUpPreexistingReportsOnAnswer(answerId, numberOfReports, Enumerable.Repeat(-1, numberOfReports).ToList());
        }

        private void SetUpPreexistingReportsOnAnswer(int answerId, int numberOfReports, List<int> reportingUserIds)
        {
            var reportsOnGivenAnswer = new List<Report>();

            for (int i = 0; i < numberOfReports; i++)
            {
                reportsOnGivenAnswer.Add(new Report() { UserInfoId = reportingUserIds[i] });
            }

            ReportRepositoryMock.Setup(m => m.GetReportsByAnswerId(answerId))
                    .Returns(reportsOnGivenAnswer);

            ReportRepositoryMock.Setup(m => m.Save(It.Is<Report>(r => r == ExpectedReport)))
                .Callback(() => reportsOnGivenAnswer.Add(ExpectedReport));

            AnswerRepositoryMock.Setup(m => m.GetById(It.Is<int>(id => id == GivenReportRequest.AnswerId.Value)))
                .Returns(ReportedAnswer);

            ReportedAnswer.AnswerId = GivenReportRequest.AnswerId.Value;
        }
        #endregion

        #region MakeReport Tests
        [Test]
        public void GivenReportRequest_WhenMakeReport_PassesCreatedReportToRepository()
        {
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(5);
            GivenReportRequest.AnswerId = 5;
            SetUpPreexistingReportsOnAnswer(answerId: 5, numberOfReports: 2, reportingUserIds: new List<int> { 1, 2});

            Target.MakeReport(GivenReportRequest);

            ReportRepositoryMock.Verify(m => m.Save(It.Is<Report>(r => r == ExpectedReport)), 
                Times.Once);
        }

        [Test]
        public void GivenReportRequestForQuestionWithOneFewerThanMaxReports_WhenMakeReport_DisableQuestion()
        {
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(5);

            GivenReportRequest.QuestionId = 10;

            SetUpPreexistingReportsOnQuestion(questionId:10, numberOfReports:4, reportingUserIds: new List<int>{1, 2, 3, 4});

            QuestionRepositoryMock.Setup(m => m.Disable(It.IsAny<int>()));

            Target.MakeReport(GivenReportRequest);

            QuestionRepositoryMock.Verify(m => m.Disable(It.Is<int>(i => i == 10)), Times.Once);
        }

        [Test]
        public void GivenReportRequestForQuestionWithTwoFewerThanMaxReports_WhenMakeReport_DoNotDisableQuestion()
        {
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(5);

            GivenReportRequest.QuestionId = 10;

            SetUpPreexistingReportsOnQuestion(questionId: 10, numberOfReports: 3, reportingUserIds: new List<int> {1, 2, 3});

            QuestionRepositoryMock.Setup(m => m.Disable(It.IsAny<int>()));

            Target.MakeReport(GivenReportRequest);

            QuestionRepositoryMock.Verify(m => m.Disable(It.Is<int>(i => i == 10)), Times.Never);
        }

        [Test]
        public void GivenReportRequestForAnswerWithOneFewerThanMaxReports_WhenMakeReport_DisableAnswer()
        {
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(5);

            GivenReportRequest.AnswerId = 10;

            SetUpPreexistingReportsOnAnswer(answerId: 10, numberOfReports: 4, reportingUserIds: new List<int>{ 1, 2, 3, 4});

            AnswerRepositoryMock.Setup(m => m.Disable(It.IsAny<int>()));

            Target.MakeReport(GivenReportRequest);

            AnswerRepositoryMock.Verify(m => m.Disable(It.Is<int>(i => i == 10)), Times.Once);
        }

        [Test]
        public void GivenReportRequestForAnswerWithTwoFewerThanMaxReports_WhenMakeReport_DoNotDisableAnswer()
        {
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(5);

            GivenReportRequest.AnswerId = 10;

            SetUpPreexistingReportsOnAnswer(answerId: 10, numberOfReports: 3, reportingUserIds:new List<int>{1, 2, 3});

            AnswerRepositoryMock.Setup(m => m.Disable(It.IsAny<int>()));

            Target.MakeReport(GivenReportRequest);

            AnswerRepositoryMock.Verify(m => m.Disable(It.Is<int>(i => i == 10)), Times.Never);
        }

        [Test]
        public void GivenReportRequestOnQuestion_WhenMakeReport_ReturnResultingNumberOfReports()
        {
            GivenReportRequest.QuestionId = 10;
            SetUpPreexistingReportsOnQuestion(questionId: 10, numberOfReports: 7, reportingUserIds: new List<int> {1, 2, 3, 4, 5, 6, 7});

            var actualResponse = Target.MakeReport(GivenReportRequest);

            Assert.AreEqual(8, actualResponse.NumberOfReports);
        }

        [Test]
        public void GivenReportRequestOnQuestion_WhenMakeReport_ReturnNumberOfReportsRequiredToDisable()
        {
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(18);
            GivenReportRequest.QuestionId = 10;
            SetUpPreexistingReportsOnQuestion(questionId: 10, numberOfReports: 7, reportingUserIds: new List<int> { 1, 2, 3, 4, 5, 6, 7});

            var actualResponse = Target.MakeReport(GivenReportRequest);

            Assert.AreEqual(18, actualResponse.ReportsDisabledOn);
        }

        [Test]
        public void GivenReportRequestOnQuestionAndUserHasReportedBefore_WhenMakeReport_SignalThatTheUserHasReportedBefore()
        {   
            GivenReportRequest.QuestionId = 10;
            GivenReportRequest.UserInfoId = 5;
            SetUpPreexistingReportsOnQuestion(questionId: 10, numberOfReports: 4, 
                reportingUserIds: new List<int> { 101, 44, 66, 5 });

            var actualResponse = Target.MakeReport(GivenReportRequest);

            Assert.IsTrue(actualResponse.UserHasReportedBefore);
        }

        [Test]
        public void GivenReportRequestOnQuestionAndUserHasNotReportedBefore_WhenMakeReport_SignalThatUserHasNotReportedBefore()
        {
            GivenReportRequest.QuestionId = 10;
            GivenReportRequest.UserInfoId = 5;
            SetUpPreexistingReportsOnQuestion(questionId: 10, numberOfReports: 4,
                reportingUserIds: new List<int> { 101, 44, 66, 102 });

            var actualResponse = Target.MakeReport(GivenReportRequest);

            Assert.IsFalse(actualResponse.UserHasReportedBefore);
        }

        [Test]
        public void Given5ReportsWith3DistinctUsersAnd5MaxReports_WhenMakeReport_DoNotDisable()
        {
            GivenReportRequest.QuestionId = 10;
            SetUpPreexistingReportsOnQuestion(questionId: 10, numberOfReports: 5, reportingUserIds: new List<int> { 5, 5, 2, 2, 9 });
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(5);

            Target.MakeReport(GivenReportRequest);

            QuestionRepositoryMock.Verify(m => m.Disable(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void GivenReportRequestOnApprovedQuestion_WhenMakeReport_DoNotDisable()
        {
            GivenReportRequest.QuestionId = 10;
            SetUpPreexistingReportsOnQuestion(10, 4, new List<int> {1, 2, 3, 4});
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(5);
            SetupModeratorApprovedQuestion();

            Target.MakeReport(GivenReportRequest);

            QuestionRepositoryMock.Verify(m => m.Disable(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void GivenReportRequestOnApprovedAnswer_WhenMakeReport_DoNotDisable()
        {
            GivenReportRequest.AnswerId = 10;
            SetUpPreexistingReportsOnAnswer(10, 4, new List<int> { 1, 2, 3, 4 });
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(5);
            SetupModeratorApprovedAnswer();

            Target.MakeReport(GivenReportRequest);

            AnswerRepositoryMock.Verify(m => m.Disable(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void GivenReportRequestOnApprovedQuestion_WhenMakeReport_ReturnIndicatedThatQuestionIsApproved()
        {
            GivenReportRequest.QuestionId = 10;
            SetUpPreexistingReportsOnQuestion(10, 2, new List<int> { 1, 2 });
            ConfigurationMock.Setup(m => m.ReportsDisabledOn).Returns(5);
            SetupModeratorApprovedQuestion();

            var actual = Target.MakeReport(GivenReportRequest);

            Assert.IsTrue(actual.ContentIsModeratorApproved);
        }

        #endregion

        [Test]
        public void GivenViewReportsRequestForQuestions_WhenGetTopReportedContent_DelegateToQuestionRepository()
        {
            QuestionRepositoryMock.Setup(m => m.GetTopReportedAndUnmoderatedContent(It.Is<int>(n => n == 10)))
                .Returns(new List<Question>());

            Target.GetTopReportedAndUnmoderatedContent<Question>(10);

            QuestionRepositoryMock.Verify(m => m.GetTopReportedAndUnmoderatedContent(It.Is<int>(n => n == 10)));
        }

        [Test]
        public void GivenViewReportsRequestForAnswers_WhenGetTopReportedContent_DelegateToAnswerRepository()
        {
            AnswerRepositoryMock.Setup(m => m.GetTopReportedAndUnmoderatedContent(It.Is<int>(n => n == 10)))
                .Returns(new List<Answer>());

            Target.GetTopReportedAndUnmoderatedContent<Answer>(10);

            AnswerRepositoryMock.Verify(m => m.GetTopReportedAndUnmoderatedContent(It.Is<int>(n => n == 10)));
        }

    }
}
