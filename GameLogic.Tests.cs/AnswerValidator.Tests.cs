using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SurrealistGames.GameLogic;

namespace GameLogic.Tests.cs
{
    [TestFixture]
    class AnswerValidatorTests
    {
        [TestCase("", "The answer cannot be empty.", TestName="GetErrors_OnEmptyString_IncludesErrorMessage")]
        [TestCase("    ", "The answer cannot be empty.", TestName="GetErrors_OnAllWhiteSpace_IncludesErrorMessage")]
        public void GetErrors_OnTestCases(string answer, string expected)
        {
            var validator = new AnswerValidator();
            var result = validator.GetErrors(answer);

            Assert.Contains(expected, result);
        }

        [Test]
        public void GetErrors_OnValidAnswer_ReturnsNoErrors()
        {
            var validator = new AnswerValidator();
            var result = validator.GetErrors("This is ok.");

            Assert.AreEqual(0, result.Count);
        }
    }
}
