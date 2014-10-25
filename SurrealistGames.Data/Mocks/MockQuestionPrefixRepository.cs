using System;
using System.Collections.Generic;
using System.Linq;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.Utility;

namespace SurrealistGames.Data.Mocks
{
    public class MockQuestionPrefixRepository : IQuestionPrefixRepository
    {
        private static List<QuestionPrefix> _prefixes = new List<QuestionPrefix>()
        {
            new QuestionPrefix()
            {
                Content = "What is the essence of life?",
                QuestionPrefixId = 1
            },

            new QuestionPrefix()
            {
                Content = "What is ugliness?",
                QuestionPrefixId = 2
            },

            new QuestionPrefix()
            {
                Content = "What is the difference between right and wrong?",
                QuestionPrefixId = 3
            }
        };

        private IRandomBehavior _rng;

        public MockQuestionPrefixRepository(IRandomBehavior rng)
        {
            _rng = rng;
        }

        public QuestionPrefix GetRandom()
        {
            return _prefixes[_rng.GetRandom(0, _prefixes.Count - 1)];
        }

        public int GetRandomId()
        {
            throw new NotImplementedException();
        }

        public void Save(QuestionPrefix prefix)
        {
            if (_prefixes.Any())
            {
                prefix.QuestionPrefixId = _prefixes.Max(x => x.QuestionPrefixId);
            }
            else
            {
                prefix.QuestionPrefixId = 1;
            }
            _prefixes.Add(prefix);
        }
    }
}