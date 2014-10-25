using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using SurrealistGames.Models;
using SurrealistGames.Utility;

namespace SurrealistGames.Repositories.Mocks
{
    public class MockQuestionSuffixRepository : Models.Interfaces.IQuestionSuffixRepository
    {
        private static List<QuestionSuffix> _suffixes = new List<QuestionSuffix>()
        {
            new QuestionSuffix()
            {
                Content = "Falling down 7 times, getting up 8.",
                QuestionSuffixId = 1
            },

            new QuestionSuffix()
            {
                Content = "Forgotten dreams.",
                QuestionSuffixId = 2
            },

            new QuestionSuffix()
            {
                Content = "Eternal suffering.",
                QuestionSuffixId = 3
            }
        };

        private IRandomBehavior _rng;

        public MockQuestionSuffixRepository(IRandomBehavior rng)
        {
            _rng = rng;
        }

        public QuestionSuffix GetRandom()
        {
            return _suffixes[_rng.GetRandom(0, _suffixes.Count-1)];
        }

        public int GetRandomId()
        {
            throw new NotImplementedException();
        }

        public void Save(QuestionSuffix suffix)
        {
            if (_suffixes.Any())
            {
                suffix.QuestionSuffixId = _suffixes.Max(x => x.QuestionSuffixId);
            }
            else
            {
                suffix.QuestionSuffixId = 1;
            }

            _suffixes.Add(suffix);
        }
    }
}