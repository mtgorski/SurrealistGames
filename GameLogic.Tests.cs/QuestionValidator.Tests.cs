using NUnit.Framework;
using SurrealistGames.GameLogic;

namespace GameLogic.Tests.cs
{
    [TestFixture]
    public class QuestionValidatorTests
    {
        [TestCase("", "The question cannot be empty.", TestName="GetErrors_OnEmptyString_ReturnsMessage")]
        [TestCase("is it true?", "The question must begin with \"What\".", 
            TestName = "GetErrors_OnQuestionWithoutWhat_ReturnsMessage")]
        
        public void GetErrors_OnTestCases_ReturnsListWithErrorMessages(string question, string expected)
        {
            var validator = new QuestionValidator();
            var result = validator.GetErrors(question);

            Assert.Contains(expected, result);
        }

        [TestCase(" What is it?")]
        [TestCase("What is it?")]
        [TestCase("What?")]
        [TestCase("what is it?")]
        [TestCase("what's the deal?")]
        [TestCase("whats going on?")]
        public void GetErrors_OnValidQuestion_ReturnsNoMessages(string question)
        {
            var validator = new QuestionValidator();
            var result = validator.GetErrors(question);

            Assert.AreEqual(0, result.Count);
        }
    }
}
