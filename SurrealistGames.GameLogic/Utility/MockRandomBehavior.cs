using System.Collections.Generic;

namespace SurrealistGames.Utility
{
    public class MockRandomBehavior : IRandomBehavior
    {
        private IEnumerator<int> _results;

        public MockRandomBehavior(int result)
        {
            var temp = new List<int>() {result};
            _results = temp.GetEnumerator();
        }

        public MockRandomBehavior(IEnumerable<int> results)
        {
            _results = results.GetEnumerator();
        }

        public int GetRandom(int min, int max)
        {
            _results.MoveNext();
            return _results.Current;
        }
    }
}