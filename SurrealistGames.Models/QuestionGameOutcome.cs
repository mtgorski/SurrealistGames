using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace SurrealistGames.Models
{
    public class QuestionGameOutcome
    {
        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}