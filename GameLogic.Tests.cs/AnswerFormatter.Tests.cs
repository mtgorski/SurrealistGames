using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SurrealistGames.GameLogic;

namespace GameLogic.Tests.cs
{
    [TestFixture]
    public class AnswerFormatterTests
    {
        [TestCase("The meaning of life.", "The meaning of life.", 
            TestName="Format_OnAlreadyFormattedAnswer_ReturnsTheAnswer")]
        [TestCase("    God    ", "God", 
            TestName="Format_OnAnswerWithLeadingAndTrailingWhiteSpace_ReturnsTrimmedAnswer")]
        public void Format_OnTestCases(string input, string expected)
        {
            var formatter = new AnswerFormatter();
            var actual = formatter.Format(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
