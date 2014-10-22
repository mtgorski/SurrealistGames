using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using SurrealistGames.GameLogic;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.Utility;
using SurrealistGames.WebUI.Controllers;

namespace Controllers.Tests.cs
{
    [TestFixture]
    class QuestionGameControllerTests
    {
        public Mock<IRandomBehavior> GetRandomMock()
        {
            var randomMock = new Mock<IRandomBehavior>();
            randomMock.Setup(m => m.GetRandom(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((x, y) => 0);
            return randomMock;
        }

        public Mock<IQuestionPrefixValidator> GetQuestionValidatorMock(bool valid)
        {
            var validatorMock = new Mock<IQuestionPrefixValidator>();
            if (valid)
            {
                validatorMock.Setup(m => m.GetErrors(It.IsAny<string>()))
                .Returns<string>(value => new List<string>());
            }
            else
            {
                validatorMock.Setup(m => m.GetErrors(It.IsAny<string>()))
                .Returns<string>(value => new List<string>() {"There was an error."});
            }

            return validatorMock;
        }

        public Mock<IQuestionSuffixValidator> GetAnswerValidatorMock(bool valid)
        {
            var validatorMock = new Mock<IQuestionSuffixValidator>();
            if (valid)
            {
                validatorMock.Setup(m => m.GetErrors(It.IsAny<string>()))
                .Returns<string>(value => new List<string>());
            }
            else
            {
                validatorMock.Setup(m => m.GetErrors(It.IsAny<string>()))
                .Returns<string>(value => new List<string>() { "There was an error." });
            }

            return validatorMock;
        }

        public Mock<IQuestionPrefixFormatter> GetQuestionFormatterMock()
        {
            var formatterMock = new Mock<IQuestionPrefixFormatter>();
            formatterMock.Setup(m => m.Format(It.IsAny<string>()))
                .Returns<string>(value => value);
            return formatterMock;
        }

        public Mock<IQuestionSuffixFormatter> GetAnswerFormatterMock()
        {
            var formatterMock = new Mock<IQuestionSuffixFormatter>();
            formatterMock.Setup(m => m.Format(It.IsAny<string>()))
                .Returns<string>(value => value);
            return formatterMock;
        }

        public Mock<IQuestionPrefixRepository> GetPrefixRepoMock()
        {
            var mock = new Mock<IQuestionPrefixRepository>();
            mock.Setup(m => m.GetRandom()).Returns(() => 
                new QuestionPrefix() {Content = "What is life?", Id = 2});
            return mock;
        }

        public Mock<IQuestionSuffixRepository> GetSuffixRepoMock()
        {
            var mock = new Mock<IQuestionSuffixRepository>();
            mock.Setup(m => m.GetRandom()).Returns(() =>
                new QuestionSuffix() { Content = "Falling down 7 times, getting up 8.", Id = 2 });
            return mock;
        }

        public QuestionGameController GetControllerUnderTest(bool validQuestion, bool validAnswer)
        {
            var prefixRepo = GetPrefixRepoMock().Object;
            var suffixRepo = GetSuffixRepoMock().Object;

            var questionValidatorMock = GetQuestionValidatorMock(validQuestion);
            var answerValidatorMock = GetAnswerValidatorMock(validAnswer);

            var questionFormatterMock = GetQuestionFormatterMock();
            var answerFormatterMock = GetAnswerFormatterMock();

            var controller = new QuestionGameController(prefixRepo, suffixRepo,
                questionValidatorMock.Object, questionFormatterMock.Object, answerValidatorMock.Object, 
                answerFormatterMock.Object);

            return controller;
        }

        public SubmitContentResult ValidSubmissionResult(string question)
        {
            var controller = GetControllerUnderTest(true, true);

            return controller.SubmitQuestion(question);
        }

        [Test]
        public void SubmitQuestion_OnValidQuestion_ReturnsQuestionResult()
        {
            var result = ValidSubmissionResult("What is life?");
  
            Assert.AreEqual("What is life?", result.GameOutcome.QuestionPrefix.Content);
            Assert.AreEqual("Falling down 7 times, getting up 8.", result.GameOutcome.QuestionSuffix.Content);
        }

        [Test]
        public void SubmitQuestion_OnValidQuestion_ReturnsSuccess()
        {
            var result = ValidSubmissionResult("What is life?");

            Assert.IsTrue(result.Success);
        }

        [Test]
        public void SubmitQuestion_OnValidQuestion_AddsQuestionToRepository()
        {
            var prefixRepo = GetPrefixRepoMock();

            var suffixRepo = GetSuffixRepoMock();
            var validator = GetQuestionValidatorMock(true);
            var answerValidator = GetAnswerValidatorMock(true);
            var questionFormatter = GetQuestionFormatterMock();
            var answerFormatter = GetAnswerFormatterMock();

            var controller = new QuestionGameController(prefixRepo.Object, suffixRepo.Object,
                validator.Object, questionFormatter.Object, answerValidator.Object, answerFormatter.Object);

            controller.SubmitQuestion("What is this?");

            prefixRepo.Verify(m => m.Save(It.IsAny<QuestionPrefix>()));
        }

        [Test]
        public void SubmitQuestion_OnInvalidQuestion_ReturnsFailureAndMessage()
        {
            var controller = GetControllerUnderTest(false, true);

            var result = controller.SubmitQuestion("");

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.ErrorMessages.Any());
        }

        [Test]
        public void SubmitQuestion_OnValidQuestions_FormatsTheQuestion()
        {
            var result = ValidSubmissionResult("Why?");

            Assert.AreEqual("Why?", result.GameOutcome.QuestionPrefix.Content);
        }

        [Test]
        public void SubmitAnswer_OnValidAnswer_ReturnsSuccess()
        {
            var controller = GetControllerUnderTest(false, true);

            var result = controller.SubmitAnswer("Because.");

            Assert.IsTrue(result.Success);
        }

        [Test]
        public void SubmitAnswer_OnValidAnswer_ReturnsResult()
        {
            var controller = GetControllerUnderTest(false, true);

            var result = controller.SubmitAnswer("Because.");

            Assert.AreEqual("Because.", result.GameOutcome.QuestionSuffix.Content);
            Assert.AreEqual("What is life?", result.GameOutcome.QuestionPrefix.Content);
        }

        [Test]
        public void SubmitAnswer_OnValidAnswer_AddsAnswerToRepo()
        {
            var suffixRepoMock = GetSuffixRepoMock();
            var controller = new QuestionGameController(GetPrefixRepoMock().Object,
                suffixRepoMock.Object, GetQuestionValidatorMock(true).Object, GetQuestionFormatterMock().Object,
                GetAnswerValidatorMock(true).Object, GetAnswerFormatterMock().Object);

            controller.SubmitAnswer("Because.");

            suffixRepoMock.Verify(m => m.Save(It.IsAny<QuestionSuffix>()));
        }

        [Test]
        public void SubmitAnswer_OnInvalidQuestion_ReturnsFailureAndMessage()
        {
            var controller = GetControllerUnderTest(true, false);

            var result = controller.SubmitAnswer("Because.");

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.ErrorMessages.Any());
        }

        [Test]
        public void SubmitAnswer_OnValidQuestion_FormatsTheQuestion()
        {
            var formatter = GetAnswerFormatterMock();
            var controller = new QuestionGameController(GetPrefixRepoMock().Object, GetSuffixRepoMock().Object,
                GetQuestionValidatorMock(false).Object, GetQuestionFormatterMock().Object,
                GetAnswerValidatorMock(true).Object, formatter.Object);

            controller.SubmitAnswer("Because.");

            formatter.Verify(m=>m.Format(It.IsAny<string>()));
        }

        
    }
}
