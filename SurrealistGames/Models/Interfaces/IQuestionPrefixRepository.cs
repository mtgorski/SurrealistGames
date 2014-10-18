using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models.Interfaces
{
    public interface IQuestionPrefixRepository
    {
        QuestionPrefix GetRandom();
        void Save(QuestionPrefix prefix);
    }
}
