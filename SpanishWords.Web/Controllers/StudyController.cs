using Microsoft.AspNetCore.Mvc;
using SpanishWords.Web.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using SpanishWords.Web.Helpers;
using SpanishWords.EntityFramework.Repositories.Infrastructure;


namespace SpanishWords.Web.Controllers
{
    public class StudyController : Controller
    {
        private IStatsRepository _statsRepository;
        private int _randomNumber;
        private readonly ILogger<StudyController> _logger;
        private IActionResult _view;

        public StudyController(IStatsRepository statsRepository, ILogger<StudyController> logger)
        {
            _statsRepository = statsRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            /*******
             *  User aswers the collection of words. Contorller will send random word which was not answered yet (the WordsToAnswer index is 
             *  not present in IndexesOfAnsweredWords list). When the number of items in IndexesOfAnsweredWords is the same as number of items in
             *  WordsToAnswer, then it means that all the words has been aswered and the Study Session is terminated.
             * *****/
            StudyViewModel study = new StudyViewModel();
            //populates list of WordsToAnswer for study Session, based on specific User
            if (LoadWordsToAnswer(study) == false) return View("NoWordsToStudy");
            if (study.WordsToAnswer.Count() == 0) return View("NoWordsToStudy");
            //generated randomNumber for the first Word
            _randomNumber = RandomNumberGenerator.GetInt32(study.WordsToAnswer.Count());
            study.IndexesOfWordsAnswered.Add(_randomNumber);
            study.RandomWord = study.WordsToAnswer.ElementAt(_randomNumber);
            study.IsFirstQuestion = true;
            return View(study);
        }
        [HttpPost]
        public IActionResult Index(StudyViewModel study)
        {
            if (IsValid(study) == false) return View("~/Views/Shared/MyError.cshtml");
            if (LoadWordsToAnswer(study) == false) return View("NoWordsToStudy");
            if (study.RandomWord.Spanish == study.Answer)
            {
                UpdateStats(study, true);
                if (_view == null) GenerateNextRandomWord(study);
            }
            else
            {
                UpdateStats(study, false);
                if (_view == null) _view = View(study);
            }
            return _view;
        }
        private bool LoadWordsToAnswer(StudyViewModel study)
        {
            study.WordsToAnswer = _statsRepository.GetAllNotLearntWords(User.FindFirstValue(ClaimTypes.NameIdentifier), 3).ToList();
            if (study.WordsToAnswer == null || study.WordsToAnswer.Count() == 0) return false;
            return true;
        }
        private bool IsValid(StudyViewModel study)
        {
            if (study == null)
            {
                _logger.LogInformation(ExceptionHelper.EMPTY_VARIABLE);
                return false;
            }
            if (study.Answer == null) study.Answer = "";
            study.Answer = study.Answer.Trim();
            return true;
        }
        private void GenerateNextRandomWord(StudyViewModel study)
        {
            //If every words in collection has been answered then terminate the study session
            if (study.IndexesOfWordsAnswered.Count() == study.WordsToAnswer.Count())
            {
                _view = View("StudyComplete");
                return;
            }
            //Generate random number until it's the one that has not been already answered
            while (study.WordsToAnswer.Count() > 1)
            {
                _randomNumber = RandomNumberGenerator.GetInt32(study.WordsToAnswer.Count());
                if (study.IndexesOfWordsAnswered.Contains(_randomNumber) == true) continue;
                else break;
            }
            study.IndexesOfWordsAnswered.Add(_randomNumber);
            ModelState.Clear();
            study.RandomWord = study.WordsToAnswer.ElementAt(_randomNumber);
            _view = View(study);
            return;
        }
        private void UpdateStats(StudyViewModel study, bool isCorrect)
        {
            if (isCorrect == true) study.WasLastAnswerCorrect = true;
            else study.WasLastAnswerCorrect = false;
            if (_statsRepository.SaveStats(study.RandomWord, isCorrect) == false)
            {
                _logger.LogInformation(ExceptionHelper.DATABASE_CONNECTION_ERROR);
                _view = View("~/Views/Shared/MyError.cshtml");
            }
            else
            {
                _view = null;
            }
        }
    }
}