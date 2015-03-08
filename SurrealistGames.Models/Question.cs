using SurrealistGames.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurrealistGames.Models
{
    public class Question : IContent
    {
        public string QuestionContent { get; set; }
        public int QuestionId { get; set; }
        public int? ApprovingUserId { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public int? RemovingUserId { get; set; }
        public DateTime? RemovedOn { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}