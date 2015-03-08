using SurrealistGames.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurrealistGames.Models
{
    public class Answer : IContent
    {
        public string AnswerContent { get; set; }
        public int AnswerId { get; set; }
        public int? ApprovingUserId { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public int? RemovingUserId { get; set; }
        public DateTime? RemovedOn { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}