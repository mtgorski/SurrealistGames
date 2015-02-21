using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models
{
    public class Report
    {
        public int ReportId { get; set; }

        public int UserInfoId {get; set;}

        public int? QuestionId { get; set; }

        public int? AnswerId { get; set; }

        public DateTime SubmittedOn { get; set; }
    }
}
