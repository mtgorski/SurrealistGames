using SurrealistGames.Models.Abstract;
using SurrealistGames.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurrealistGames.Models
{
    public class Answer : Content, IContent
    {
        public string AnswerContent { get; set; }
        public int AnswerId { get; set; }

        public override int Id
        {
            get
            {
                return AnswerId;
            }
            set
            {
                AnswerId = value;
            }
        }

        public override string Value
        {
            get
            {
                return AnswerContent;
            }
            set
            {
                AnswerContent = value;
            }
        }
    }
}