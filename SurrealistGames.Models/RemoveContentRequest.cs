using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models
{
    public class RemoveContentRequest
    {
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public int RequestingUserId { get; set; }

        public int ContentId
        {
            get
            {
                return QuestionId.HasValue ? QuestionId.Value : AnswerId.Value;
            }
        }
    }
}
