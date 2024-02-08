using EFDataAccess.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpanishWords.Web.Models;
using EFDataAccess.Repositories.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SpanishWords.EntityFramework.Repositories.Infrastructure;
using SpanishWords.EntityFramework.Repositories;
using SpanishWords.Web.Helpers;
using SpanishWords.Models.Tables;

namespace SpanishWords.Web.Controllers
{
    [Authorize]
    public class WordController : Controller
    {

        private IWordRepository _wordRepository;
        private IStatsRepository _statsRepository;
        private const int CORRECT_ANSWERS_TO_LEARN = 3;
        private readonly ILogger<WordController> _logger;

        public WordController(IWordRepository wordRepository, IStatsRepository statsRepository, ILogger<WordController> logger)
        {
            _wordRepository = wordRepository;
            _statsRepository = statsRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            WordViewModel wordViewModel = new WordViewModel();

            wordViewModel.Words = _wordRepository.GetAllWords(User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList(); //string z userId

            for (int i = 0; i < wordViewModel.Words.Count(); i++)
            {
                wordViewModel.TimesCorrect.Add(_wordRepository.GetWordsTimesCorrect(wordViewModel.Words.ElementAt(i).Id));
                wordViewModel.TimesIncorrect.Add(_wordRepository.GetWordsTimesIncorrect(wordViewModel.Words.ElementAt(i).Id));
                wordViewModel.TimesTrained.Add(wordViewModel.TimesCorrect.Last() + wordViewModel.TimesIncorrect.Last());
            }

            wordViewModel.TimesCorrectForLearning = 3;

            return View(wordViewModel);
        }
        public IActionResult Add()
        {
            WordViewModel model = new();

            model.GrammaticalGenders = _wordRepository.GetGrammaticalGenders().Select(a => new SelectListItem()
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });

            model.LexicalCategories = _wordRepository.GetLexicalCategories().Select(a => new SelectListItem()
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(WordViewModel wordViewModel)
        {
            if (wordViewModel.Word.Spanish == null || wordViewModel.Word.Spanish.Trim() == "" 
                || wordViewModel.Word.English == null || wordViewModel.Word.English.Trim() == "" 
                || wordViewModel.Word.GrammaticalGenderId == null || wordViewModel.Word.LexicalCategoryId == null
                || wordViewModel.Word.GrammaticalGenderId == 0 || wordViewModel.Word.LexicalCategoryId == 0)
            {
                _logger.LogError(ExceptionHelper.EMPTY_VARIABLE);
                return View("WordError");
            }
 
            wordViewModel.Word.Spanish = wordViewModel.Word.Spanish.Trim();
            wordViewModel.Word.English = wordViewModel.Word.English.Trim();

            wordViewModel.Word.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            wordViewModel.Word.StatisticId = _wordRepository.CreateAndAddStatistic(CORRECT_ANSWERS_TO_LEARN).Id;
            if (wordViewModel.Word.StatisticId == -1) return View("WordError");

            if (_wordRepository.Add(wordViewModel.Word) == false)
            {
                _logger.LogError(ExceptionHelper.DATABASE_CONNECTION_ERROR);
                return View("WordError");
            }

            return RedirectToAction("Add");
        }
        public IActionResult Edit(int id)
        {
            WordViewModel word = new WordViewModel();
            word.Word = _wordRepository.GetWordById(id);

            word.GrammaticalGenders = _wordRepository.GetGrammaticalGenders().Select(a => new SelectListItem()
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });
            word.LexicalCategories = _wordRepository.GetLexicalCategories().Select(a => new SelectListItem()
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });

            return View("Add", word);
        }
        [HttpPost]
        public IActionResult Edit(WordViewModel model)
        {
            model.Word.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            if (_wordRepository.Edit(model.Word) == false)
            {
                _logger.LogError(ExceptionHelper.DATABASE_CONNECTION_ERROR);
                return View("WordError");
            }

            return RedirectToAction("Index", "Word");
        }
        public IActionResult Delete(int id)
        {
            Word? word = _wordRepository.GetWordById(id);           

            if (word == null || _wordRepository.Delete(word) == false)
            {
                _logger.LogError(ExceptionHelper.DATABASE_CONNECTION_ERROR);
                return View("WordError");
            }
           
            return RedirectToAction("Index", "Word");
        }        
        public IActionResult RestartProgressForAllWords()
        {
            if (_wordRepository.RestartProgressForAll(User.FindFirstValue(ClaimTypes.NameIdentifier)) == false)
            {
                _logger.LogError(ExceptionHelper.DATABASE_CONNECTION_ERROR);
                return View("WordError");
            }
            
            WordViewModel wordViewModel = new WordViewModel();
            LoadAllWordsFromRepository(wordViewModel);
            return View("Index", wordViewModel);
        }
        public IActionResult RestartProgressForOneWord(int id)
        {
            if (_wordRepository.RestartProgress(id) == false)
            {
                _logger.LogError(ExceptionHelper.DATABASE_CONNECTION_ERROR);
                return View("WordError");
            }
            
            WordViewModel wordViewModel = new WordViewModel();
            LoadAllWordsFromRepository(wordViewModel);            
            return RedirectToAction("Index", "Word");
        }
        private void LoadAllWordsFromRepository(WordViewModel wordViewModel)
        {
            //how to check if operation was successful???
            wordViewModel.Words = _wordRepository.GetAllWords(User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList(); 
            for (int i = 0; i < wordViewModel.Words.Count(); i++)
            {
                int id = wordViewModel.Words.ElementAt(i).Id;
                wordViewModel.TimesCorrect.Add(_wordRepository.GetWordsTimesCorrect(id));
                wordViewModel.TimesIncorrect.Add(_wordRepository.GetWordsTimesIncorrect(id));
                wordViewModel.TimesTrained.Add(_statsRepository.GetWordsTotalTrainedTimes(id));
            }
            wordViewModel.TimesCorrectForLearning = CORRECT_ANSWERS_TO_LEARN;
        }
    }
}