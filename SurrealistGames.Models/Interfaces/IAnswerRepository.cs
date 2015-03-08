using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models.Interfaces
{
    public interface IAnswerRepository : IContentRepository
    {
        Answer GetRandom();
        void Save(Answer prefix);
        Answer GetById(int answerId);
    }
}
