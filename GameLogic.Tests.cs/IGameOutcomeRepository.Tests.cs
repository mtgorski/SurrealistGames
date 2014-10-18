using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SurrealistGames.Models;
using SurrealistGames.Repositories;
using SurrealistGames.Repositories.Mocks;
using SurrealistGames.Utility;
using SurrealistGames.GameLogic;
    
namespace Repositories.Tests.cs
{
    [TestFixture]
    public class IGameOutcomeRepositoryTests
    {
        [Test]
        public void GetOutcome_NoArgument_ReturnsOutcomeFromQuestionAndAnswerRepos()
        {
            var suffixRepo = new MockQuestionSuffixRepository(new MockRandomBehavior(1));
            var prefixRepo = new MockQuestionPrefixRepository(new MockRandomBehavior(0));
            var outcomeRepo = new QuestionGameOutcomeGenerator(prefixRepo, suffixRepo);

            var result = outcomeRepo.GetOutcome();

            Assert.AreEqual("What is the essence of life?", result.QuestionPrefix.Content);
            Assert.AreEqual( "Forgotten dreams.", result.QuestionSuffix.Content);
        }

        [Test]
        public void GetOutcome_PrefixGiven_ReturnsPrefixAndRandomSuffix()
        {
            var suffixRepo = new MockQuestionSuffixRepository(new MockRandomBehavior(1));
            var prefixRepo = new MockQuestionPrefixRepository(new MockRandomBehavior(0));
            var outcomeRepo = new QuestionGameOutcomeGenerator(prefixRepo, suffixRepo);
            var prefix = new QuestionPrefix() {Content = "What are you?"};

            var result = outcomeRepo.GetOutcome(prefix);

            Assert.AreEqual(result.QuestionPrefix.Content, "What are you?");
            Assert.AreEqual(result.QuestionSuffix.Content, "Forgotten dreams.");
        }

        [Test]
        public void GetOutcome_SuffixGiven_ReturnsSuffixAndRandomPrefix()
        {
            var suffixRepo = new MockQuestionSuffixRepository(new MockRandomBehavior(1));
            var prefixRepo = new MockQuestionPrefixRepository(new MockRandomBehavior(0));
            var outcomeRepo = new QuestionGameOutcomeGenerator(prefixRepo, suffixRepo);
            var suffix = new QuestionSuffix() {Content = "A living hell."};

            var result = outcomeRepo.GetOutcome(suffix);

            Assert.AreEqual(result.QuestionPrefix.Content, "What is the essence of life?");
            Assert.AreEqual(result.QuestionSuffix.Content, "A living hell.");
        }
    }
}
