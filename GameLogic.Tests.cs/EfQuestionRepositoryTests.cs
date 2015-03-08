using Moq;
using NUnit.Framework;
using SurrealistGames.Data;
using SurrealistGames.Models;
using SurrealistGames.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Tests.cs
{
    [TestFixture]
    class EfQuestionRepositoryTests
    {

        public Mock<IEfDbContext> ContextMock { get; set; }
        public Mock<DbSet<Report>> ReportsMock { get; set; }
        public Mock<DbSet<Question>> QuestionsMock { get; set; }
        public EfQuestionRepository Target { get; set; }
        public List<Report> SeedReports { get; set; }

        public Report GivenReport { get; set; }

        [SetUp]
        public void SetUp()
        {
            ContextMock = new Mock<IEfDbContext>();
            GivenReport = new Report();
            ReportsMock = new Mock<DbSet<Report>>();
            QuestionsMock = new Mock<DbSet<Question>>();

            var rng = new Mock<IRandomBehavior>();
            Target = new EfQuestionRepository(rng.Object, ContextMock.Object);

            ContextMock.Setup(m => m.Reports).Returns(ReportsMock.Object);
            ContextMock.Setup(m => m.Questions).Returns(QuestionsMock.Object);
        }

        [Test]
        public void GivenNumberOfItemsToReturn_WhenGetTopReported_ReturnUnapprovedQuestions()
        {
            var questions = new List<Question> {
                new Question {
                    QuestionId = 1
                }, 
                new Question {
                    QuestionId =2 
                }, 
                new Question {
                    QuestionId = 3, ApprovingUserId = 4
                }, 
                new Question {
                    QuestionId = 4
                }
            };
            questions.ForEach(q => q.Reports = new List<Report>());

            MockDbSetExtensions.SetupData<Question>(QuestionsMock, questions);

            var response = Target.GetTopReportedAndUnmoderatedContent(10);

            Assert.IsTrue(response.All(q => q.ApprovingUserId == null));
        }

        [Test]
        public void GivenNumberOfItemsToReturn_WhenGetTopReported_ReturnUnremovedQuestions()
        {
            var questions = new List<Question> 
            {
                new Question 
                {
                    RemovingUserId = 3
                }, 

                new Question
                {
                  
                },

                new Question
                {
                    RemovingUserId = 2
                }, 

                new Question()
            };
            questions.ForEach(q => q.Reports = new List<Report>());
            QuestionsMock.SetupData<Question>(questions);

            var result = Target.GetTopReportedAndUnmoderatedContent(10);

            Assert.IsTrue(result.All(q => !q.RemovingUserId.HasValue));
        }

        [Test]
        public void GivenNumberOfItemsToReturnIs3And5Questions_WhenGetTopUnmoderatedContent_Return3Questions()
        {
            var questions = new List<Question>
            {
                new Question(), new Question(), new Question(), new Question(), new Question()
            };
            questions.ForEach(q => q.Reports = new List<Report>());
            QuestionsMock.SetupData<Question>(questions);

            var result = Target.GetTopReportedAndUnmoderatedContent(3);

            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void GivenNumberOfItemsToReturn_WhenGetTopUnmoderatedContent_ReturnQuestionsOrderedByNumberOfResults()
        {
            #region TestData
            var questions = new List<Question> ()
            {
                new Question ()
                {
                    QuestionId = 1
                }, 
                new Question ()
                {
                    QuestionId =2 
                }, 
                new Question ()
                {
                    QuestionId = 3
                }, 
                new Question ()
                {
                    QuestionId = 4
                }
            };

            var reports = new List<Report>()
            {
                new Report()
                {
                    QuestionId = 1
                },
 
                new Report()
                {
                    QuestionId = 3
                }, 

                new Report()
                {
                    QuestionId = 3
                },

                new Report ()
                {
                    QuestionId = 3
                }, 

                new Report ()
                {
                    QuestionId = 2
                }, 

                new Report()
                {
                    QuestionId = 2
                }
            };
            #endregion
            foreach(var question in questions)
            {
                question.Reports = reports.Where(r => r.QuestionId == question.QuestionId).ToList();
            }

            QuestionsMock.SetupData<Question>(questions);
            ReportsMock.SetupData<Report>(reports);
            
            var result = Target.GetTopReportedAndUnmoderatedContent(10);
            var questionIds = result.Select(x => x.Id).ToList();

            CollectionAssert.AreEqual(new List<int> { 3, 2, 1, 4 }, questionIds);
        }
    }
}
