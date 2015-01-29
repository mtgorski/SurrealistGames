using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;

namespace SurrealistGames.GameLogic.GameLogic
{
    public class QuestionGameOutcomeGenerator
    {
        private IQuestionRepository _prefixRepo;
        private IAnswerRepository _suffixRepo;

        public QuestionGameOutcomeGenerator(IQuestionRepository prefixRepo, 
            IAnswerRepository suffixRepo)
        {
            _prefixRepo = prefixRepo;
            _suffixRepo = suffixRepo;
        }

        public QuestionGameOutcome GetOutcome(Models.Question prefix)
        {
            return new QuestionGameOutcome()
            {
                Question = prefix,
                Answer = _suffixRepo.GetRandom()
            };
        }

        public Models.QuestionGameOutcome GetOutcome(Models.Answer suffix)
        {
            return new QuestionGameOutcome()
            {
                Question = _prefixRepo.GetRandom(),
                Answer = suffix
            };
        }

        public Models.QuestionGameOutcome GetOutcome()
        {
            return new QuestionGameOutcome()
            {
                Question = _prefixRepo.GetRandom(),
                Answer = _suffixRepo.GetRandom()
            };
        }
    }
}