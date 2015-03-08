using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models.Interfaces
{
    public interface IQuestionRepository : IContentRepository
    {
        Question GetRandom();
        void Save(Question prefix);
        Question GetById(int questionId);
    }
}
