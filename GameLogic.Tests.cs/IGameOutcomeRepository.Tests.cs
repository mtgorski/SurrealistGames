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
            var prefixRepo = new Mock<IQuestionPrefixRepository>();
            var suffixRepo = new Mock<IQuestionSuffixRepository>();
            prefixRepo.Setup(x => x.GetRandom())
                .Returns(new QuestionPrefix()
                {
                    QuestionPrefixContent = "What is the essence of life?",
                });
            suffixRepo.Setup(x => x.GetRandom())
                .Returns(new QuestionSuffix()
                {
                    QuestionSuffixContent = "Forgotten dreams."
                });

            var outcomeGenerator = new QuestionGameOutcomeGenerator(prefixRepo.Object, suffixRepo.Object);

            var result = outcomeGenerator.GetOutcome();

            Assert.AreEqual("What is the essence of life?", result.QuestionPrefix.QuestionPrefixContent);
            Assert.AreEqual("Forgotten dreams.", result.QuestionSuffix.QuestionSuffixContent);
        }

        [Test]
        public void GetOutcome_PrefixGiven_ReturnsPrefixAndRandomSuffix()
        {
            var prefixRepo = new Mock<IQuestionPrefixRepository>();
            var suffixRepo = new Mock<IQuestionSuffixRepository>();
            suffixRepo.Setup(m => m.GetRandom())
                .Returns(new QuestionSuffix()
                {
                    QuestionSuffixContent = "Forgotten dreams."
                });
            var outcomeRepo = new QuestionGameOutcomeGenerator(prefixRepo.Object, suffixRepo.Object);


            var prefix = new QuestionPrefix() { QuestionPrefixContent = "What are you?" };

            var result = outcomeRepo.GetOutcome(prefix);

            Assert.AreEqual(result.QuestionPrefix.QuestionPrefixContent, "What are you?");
            Assert.AreEqual(result.QuestionSuffix.QuestionSuffixContent, "Forgotten dreams.");
        }

        [Test]
        public void GetOutcome_SuffixGiven_ReturnsSuffixAndRandomPrefix()
        {
            var prefixRepo = new Mock<IQuestionPrefixRepository>();
            var suffixRepo = new Mock<IQuestionSuffixRepository>();
            prefixRepo.Setup(m => m.GetRandom())
               .Returns(new QuestionPrefix()
               {
                   QuestionPrefixContent = "What is the essence of life?"
               });

            var outcomeRepo = new QuestionGameOutcomeGenerator(prefixRepo.Object, suffixRepo.Object);
            var suffix = new QuestionSuffix() { QuestionSuffixContent = "A living hell." };

            var result = outcomeRepo.GetOutcome(suffix);

            Assert.AreEqual(result.QuestionPrefix.QuestionPrefixContent, "What is the essence of life?");
            Assert.AreEqual(result.QuestionSuffix.QuestionSuffixContent, "A living hell.");
        }
    }
}
