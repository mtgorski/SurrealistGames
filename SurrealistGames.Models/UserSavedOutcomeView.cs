using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models
{
    public class UserSavedOutcomeView
    {
        public int SavedQuestionId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
