using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;

namespace SurrealistGames.GameLogic.GameLogic
{
    public class QuestionGameOutcomeGenerator
    {
        private IQuestionPrefixRepository _prefixRepo;
        private IQuestionSuffixRepository _suffixRepo;

        public QuestionGameOutcomeGenerator(IQuestionPrefixRepository prefixRepo, 
            IQuestionSuffixRepository suffixRepo)
        {
            _prefixRepo = prefixRepo;
            _suffixRepo = suffixRepo;
        }

        public QuestionGameOutcome GetOutcome(Models.QuestionPrefix prefix)
        {
            return new QuestionGameOutcome()
            {
                QuestionPrefix = prefix,
                QuestionSuffix = _suffixRepo.GetRandom()
            };
        }

        public Models.QuestionGameOutcome GetOutcome(Models.QuestionSuffix suffix)
        {
            return new QuestionGameOutcome()
            {
                QuestionPrefix = _prefixRepo.GetRandom(),
                QuestionSuffix = suffix
            };
        }

        public Models.QuestionGameOutcome GetOutcome()
        {
            return new QuestionGameOutcome()
            {
                QuestionPrefix = _prefixRepo.GetRandom(),
                QuestionSuffix = _suffixRepo.GetRandom()
            };
        }
    }
}