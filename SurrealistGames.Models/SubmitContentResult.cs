using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurrealistGames.Models
{
    public class SubmitContentResult
    {
        public QuestionGameOutcome GameOutcome { get; set; }
        public bool Success { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}