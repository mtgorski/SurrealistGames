using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.GameLogic
{
    public interface IQuestionSuffixValidator
    {
        List<string> GetErrors(string content);
    }
}
