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
    public class QuestionFormatterTests
    {
        [TestCase("What is life?", "What is life?", 
            TestName="Format_OnAlreadyFormattedString_ReturnsResult")]
        [TestCase("  What is life?", "What is life?", 
            TestName="Format_OnStringWithLeadingWhitespace_ReturnsTrimmedString")]
        [TestCase("What is life?  ", "What is life?", 
            TestName="Format_OnStringWithTrailingWhitespace_ReturnsTrimmedString")]
        [TestCase("what is life?", "What is life?", 
            TestName="Format_OnStringWithLowerCaseFirstLetter_ReturnsCapitalizedString")]
        [TestCase("what is life  ", "What is life?", 
            TestName="Format_OnStringWithNoQuestionMarkAndWhitespace_ReturnsStringTrimmedAndWithQuestionMark")]

        public void Format_TestCases(string input, string expected)
        {
            var formatter = new QuestionFormatter();
            var actual = formatter.Format(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
