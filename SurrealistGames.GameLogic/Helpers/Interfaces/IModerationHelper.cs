using SurrealistGames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.GameLogic.Helpers.Interfaces
{
    public interface IModerationHelper
    {
        RemoveContentResponse RemoveContent(RemoveContentRequest request);
    }
}
