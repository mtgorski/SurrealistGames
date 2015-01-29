using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models
{
    public class SavedQuestionGameResult
    {
        public int SavedQuestionGameResultId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int UserInfoId { get; set; }

        
        public virtual Question Question { get; set; }
        public virtual Answer Answer { get; set; }

    }
}
