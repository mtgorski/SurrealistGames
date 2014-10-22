using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurrealistGames.Utility
{
    public interface IRandomBehavior
    {
        int GetRandom(int min, int max);
    }
}