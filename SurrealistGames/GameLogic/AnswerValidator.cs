using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurrealistGames.GameLogic
{
    public class AnswerValidator : IQuestionSuffixValidator
    {
        public List<string> GetErrors(string content)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(content))
            {
                errors.Add("The answer cannot be empty.");
            }

            return errors;
        }
    }
}