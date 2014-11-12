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
        public QuestionPrefix QuestionPrefix { get; set; }
        public QuestionSuffix QuestionSuffix { get; set; }
    }
}