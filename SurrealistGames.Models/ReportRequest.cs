using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models
{
    public class ReportRequest
    {
        public int? QuestionId { get; set; }

        public int? AnswerId { get; set; }

        public int UserInfoId { get; set; }
    }
}
