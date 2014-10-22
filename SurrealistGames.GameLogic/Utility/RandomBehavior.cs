using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurrealistGames.Utility
{
    public class RandomBehavior : IRandomBehavior
    {
        private static Random _rng = new Random();

        /// <summary>
        /// Gets an integer between min and max, inclusive.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int GetRandom(int min, int max)
        {
            return _rng.Next(min, max + 1);
        }
    }
}