using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurrealistGames.GameLogic
{
    public class QuestionFormatter : IQuestionPrefixFormatter
    {
        public string Format(string question)
        {
            var result = question.Trim();
            result = result[0].ToString().ToUpper() + result.Substring(1);
            if (result.Last() != '?')
            {
                result += '?';
            }
            return result;
        }
    }
}