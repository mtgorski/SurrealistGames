using Moq;
using NUnit.Framework;
using SurrealistGames.GameLogic.GameLogic;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.Utility;

namespace Repositories.Tests.cs
{
    [TestFixture]
    public class IGameOutcomeRepositoryTests
    {
        [Test]
        public void GetOutcome_NoArgument_ReturnsOutcomeFromQuestionAndAnswerRepos()
        {
            var prefixRepo = new Mock<IQuestionRepository>();
            var suffixRepo = new Mock<IAnswerRepository>();
            prefixRepo.Setup(x => x.GetRandom())
                .Returns(new Question()
                {
                    QuestionContent = "What is the essence of life?",
                });
            suffixRepo.Setup(x => x.GetRandom())
                .Returns(new Answer()
                {
                    AnswerContent = "Forgotten dreams."
                });

            var outcomeGenerator = new QuestionGameOutcomeGenerator(prefixRepo.Object, suffixRepo.Object);

            var result = outcomeGenerator.GetOutcome();

            Assert.AreEqual("What is the essence of life?", result.Question.QuestionContent);
            Assert.AreEqual("Forgotten dreams.", result.Answer.AnswerContent);
        }

        [Test]
        public void GetOutcome_PrefixGiven_ReturnsPrefixAndRandomSuffix()
        {
            var prefixRepo = new Mock<IQuestionRepository>();
            var suffixRepo = new Mock<IAnswerRepository>();
            suffixRepo.Setup(m => m.GetRandom())
                .Returns(new Answer()
                {
                    AnswerContent = "Forgotten dreams."
                });
            var outcomeRepo = new QuestionGameOutcomeGenerator(prefixRepo.Object, suffixRepo.Object);


            var prefix = new Question() { QuestionContent = "What are you?" };

            var result = outcomeRepo.GetOutcome(prefix);

            Assert.AreEqual(result.Question.QuestionContent, "What are you?");
            Assert.AreEqual(result.Answer.AnswerContent, "Forgotten dreams.");
        }

        [Test]
        public void GetOutcome_SuffixGiven_ReturnsSuffixAndRandomPrefix()
        {
            var prefixRepo = new Mock<IQuestionRepository>();
            var suffixRepo = new Mock<IAnswerRepository>();
            prefixRepo.Setup(m => m.GetRandom())
               .Returns(new Question()
               {
                   QuestionContent = "What is the essence of life?"
               });

            var outcomeRepo = new QuestionGameOutcomeGenerator(prefixRepo.Object, suffixRepo.Object);
            var suffix = new Answer() { AnswerContent = "A living hell." };

            var result = outcomeRepo.GetOutcome(suffix);

            Assert.AreEqual(result.Question.QuestionContent, "What is the essence of life?");
            Assert.AreEqual(result.Answer.AnswerContent, "A living hell.");
        }
    }
}
