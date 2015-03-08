using SurrealistGames.Models.Abstract;
using SurrealistGames.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurrealistGames.Models
{
    public class Question : Content, IContent
    {
        public string QuestionContent { get; set; }
        public int QuestionId { get; set; }

        public override string Value
        {
            get
            {
                return QuestionContent;
            }
            set
            {
                QuestionContent = value;
            }
        }

        public override int Id
        {
            get
            {
                return QuestionId;
            }
            set
            {
               QuestionId = QuestionId;
            }
        }

    }
}