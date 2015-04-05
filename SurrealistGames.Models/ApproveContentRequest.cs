using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models
{
    public class ApproveContentRequest
    {
        public int RequestingUserId { get; set; }

        public int? AnswerId { get; set; }
        public int? QuestionId { get; set; }

        public int ContentId
        {
            get
            {
                return AnswerId.HasValue ? AnswerId.Value : QuestionId.Value;
            }
        }

        public Type ContentType
        {
            get
            {
                if(AnswerId.HasValue)
                {
                    return typeof(Answer);
                }
                else if(QuestionId.HasValue)
                {
                    return typeof(Question);
                }

                throw new InvalidOperationException("Content type cannot be determined.");
            }
        }
    }
}
