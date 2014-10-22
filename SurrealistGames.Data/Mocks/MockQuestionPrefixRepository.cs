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
                Id = 1
            },

            new QuestionPrefix()
            {
                Content = "What is ugliness?",
                Id = 2
            },

            new QuestionPrefix()
            {
                Content = "What is the difference between right and wrong?",
                Id = 3
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
                prefix.Id = _prefixes.Max(x => x.Id);
            }
            else
            {
                prefix.Id = 1;
            }
            _prefixes.Add(prefix);
        }
    }
}