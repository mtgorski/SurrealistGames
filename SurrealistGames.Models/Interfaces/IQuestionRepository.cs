using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models.Interfaces
{
    public interface IQuestionRepository
    {
        Question GetRandom();
        void Save(Question prefix);
        void Disable(int questionId);
    }
}
