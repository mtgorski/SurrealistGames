using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SurrealistGames.GameLogic
{
    public class QuestionValidator : IQuestionValidator
    {
        public List<string> GetErrors(string question)
        {
            var result = new List<string>();

            if (string.IsNullOrWhiteSpace(question))
            {
                result.Add("The question cannot be empty.");
                return result;
            }

            var firstword = question.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries)[0]
                                    .ToUpper();

            if (firstword.Length < 4 || firstword.Substring(0, 4) != "WHAT")
            {
                if (!(firstword.Length == 5 && firstword == "WHAT?"))
                {
                    result.Add("The question must begin with \"What\".");
                }                
            }

            return result;
        }
    }
}