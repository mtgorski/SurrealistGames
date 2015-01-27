using System.Linq;
using System.Web.Mvc;
using SurrealistGames.GameLogic;
using SurrealistGames.GameLogic.GameLogic;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;

namespace SurrealistGames.WebUI.Controllers
{
    public class QuestionGameController : Controller
    {
        private QuestionGameOutcomeGenerator _generator;
        private IQuestionPrefixRepository _prefixRepo;
        private IQuestionSuffixRepository _suffixRepo;
        private IQuestionPrefixValidator _questionValidator;
        private IQuestionPrefixFormatter _questionFormatter;
        private IQuestionSuffixFormatter _answerFormatter;
        private IQuestionSuffixValidator _answerValidator;

        public QuestionGameController(IQuestionPrefixRepository prefixRepo, IQuestionSuffixRepository suffixRepo,
              IQuestionPrefixValidator questionValidator, IQuestionPrefixFormatter questionFormatter,
              IQuestionSuffixValidator answerValidator, IQuestionSuffixFormatter answerFormatter)
        {
            _prefixRepo = prefixRepo;
            _suffixRepo = suffixRepo;
            _questionValidator = questionValidator;
            _questionFormatter = questionFormatter;
            _answerValidator = answerValidator;
            _answerFormatter = answerFormatter;
            _generator = new QuestionGameOutcomeGenerator(_prefixRepo, _suffixRepo);

        }

        public ActionResult Index()
        {
            return View(new SavedQuestionGameResult());
        }

        // GET: QuestionGame
        public ActionResult Play()
        {
            return View(new SavedQuestionGameResult());
        }

        [HttpPost]
        public JsonResult SubmitQuestionAction(string questionContent)
        {
            var result = SubmitQuestion(questionContent);
            return Json(result);
        }

        public SubmitContentResult SubmitQuestion(string questionContent)
        {
            var result = new SubmitContentResult();

            result.ErrorMessages = _questionValidator.GetErrors(questionContent);

            if (!result.ErrorMessages.Any())
            {
                var formattedQuestion = _questionFormatter.Format(questionContent);
                var prefix = new QuestionPrefix() { QuestionPrefixContent = formattedQuestion };
                result.GameOutcome = _generator.GetOutcome(prefix);
                result.Success = true;
                _prefixRepo.Save(prefix);
            }

            return result;
        }

        [HttpPost]
        public JsonResult SubmitAnswerAction(string answerContent)
        {
            var result = SubmitAnswer(answerContent);
            return Json(result);
        }

        public SubmitContentResult SubmitAnswer(string answerContent)
        {
            var result = new SubmitContentResult();

            result.ErrorMessages = _answerValidator.GetErrors(answerContent);

            if (!result.ErrorMessages.Any())
            {
                var formattedAnswer = _answerFormatter.Format(answerContent);
                var suffix = new QuestionSuffix() { QuestionSuffixContent = formattedAnswer };
                result.GameOutcome = _generator.GetOutcome(suffix);
                result.Success = true;
                _suffixRepo.Save(suffix);
            }

            return result;
        }

        public JsonResult GetRandomOutcome()
        {
            var outcome = new QuestionGameOutcome()
            {
                QuestionSuffix = _suffixRepo.GetRandom(),
                QuestionPrefix = _prefixRepo.GetRandom()
            };

            return Json(outcome, JsonRequestBehavior.AllowGet);
        }
    }
}