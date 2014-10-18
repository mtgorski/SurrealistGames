using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurrealistGames.GameLogic
{
    public class AnswerFormatter : IQuestionSuffixFormatter
    {
        public string Format(string content)
        {
            return content.Trim();
        }
    }
}