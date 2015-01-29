using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.GameLogic
{
    public interface IQuestionValidator
    {
        List<string> GetErrors(string content);
    }
}
