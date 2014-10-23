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
        public int QuestionPrefixId { get; set; }
        public int QuestionSuffixId { get; set; }
        public int UserInfoId { get; set; }
    }
}
