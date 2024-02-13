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
using SpanishWords.Models.DTOs;

namespace SpanishWords.Web.Controllers
{
    [Authorize]
    public class WordController : Controller
    {
        private IWordRepository _wordRepository;
        private IStatsRepository _statsRepository;
        private readonly ILogger<WordController> _logger;
        private AddWordErrorViewModel _addWordErrorViewModel = new AddWordErrorViewModel();        

        public WordController(IWordRepository wordRepository, IStatsRepository statsRepository, ILogger<WordController> logger)
        {
            _wordRepository = wordRepository;
            _statsRepository = statsRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            WordViewModel wordViewModel = new WordViewModel();

            wordViewModel.WordsView = _wordRepository.GetAllWordsWithStatsFromView(User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();

            wordViewModel.TimesCorrectForLearning = SettingsHelper.CORRECT_NUMBER_FOR_LEARNING;

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
            if (CheckIfAddedWordIsValid(wordViewModel) != true)
            {
                _logger.LogError(ExceptionHelper.EMPTY_VARIABLE);
                return View("WordError", _addWordErrorViewModel); //todo: implement in view
            }
 
            wordViewModel.Word.Spanish = wordViewModel.Word.Spanish.Trim();
            wordViewModel.Word.English = wordViewModel.Word.English.Trim();

            wordViewModel.Word.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CreateStatisticDTO createdStatisticDTO = _wordRepository.CreateStatistic(SettingsHelper.CORRECT_NUMBER_FOR_LEARNING);
            wordViewModel.Word.StatisticId = createdStatisticDTO.Statistic.Id;
            if (createdStatisticDTO.Success == false) return View("WordError", _addWordErrorViewModel);

            if (_wordRepository.Add(wordViewModel.Word) == false)
            {
                _logger.LogError(ExceptionHelper.DATABASE_CONNECTION_ERROR);
                return View("WordError", _addWordErrorViewModel);
            }

            return RedirectToAction("Add");
        }

        private bool CheckIfAddedWordIsValid(WordViewModel wordViewModel)
        {
            _addWordErrorViewModel.ClearMessages();
            _addWordErrorViewModel.MessageTitle = ErrorViewMessageHelper.ADD_WORD_TITLE_MESSAGE;
            bool isValid = true;
            if (wordViewModel.Word.Spanish == null || wordViewModel.Word.Spanish.Trim() == "")
            {
                _addWordErrorViewModel.Messages.Add(ErrorViewMessageHelper.ADD_WORD_SPANISH_WORD_ERROR);
                isValid =  false;
            }
            if (wordViewModel.Word.English == null || wordViewModel.Word.English.Trim() == "")
            {
                _addWordErrorViewModel.Messages.Add(ErrorViewMessageHelper.ADD_WORD_ENGLISH_WORD_ERROR);
                isValid = false;
            }
            if (wordViewModel.Word.GrammaticalGenderId == null || wordViewModel.Word.LexicalCategoryId == null)
            {
                _addWordErrorViewModel.Messages.Add(ErrorViewMessageHelper.ADD_WORD_GRAMMATICAL_ERROR);
                isValid = false;
            }
            if (wordViewModel.Word.GrammaticalGenderId == 0 || wordViewModel.Word.LexicalCategoryId == 0)
            {
                _addWordErrorViewModel.Messages.Add(ErrorViewMessageHelper.ADD_WORD_LEXICAL_ERROR);
                isValid = false;
            }
            return isValid;
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
                return View("WordError", _addWordErrorViewModel);
            }

            return RedirectToAction("Index", "Word");
        }
        public IActionResult Delete(int id)
        {
            Word? word = _wordRepository.GetWordById(id);           

            if (word == null || _wordRepository.Delete(word) == false)
            {
                _logger.LogError(ExceptionHelper.DATABASE_CONNECTION_ERROR);
                return View("WordError", _addWordErrorViewModel);
            }
           
            return RedirectToAction("Index", "Word");
        }        
        public IActionResult RestartProgressForAllWords()
        {
            if (_wordRepository.RestartProgressForAll(User.FindFirstValue(ClaimTypes.NameIdentifier)) == false)
            {
                _logger.LogError(ExceptionHelper.DATABASE_CONNECTION_ERROR);
                return View("WordError", _addWordErrorViewModel);
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
                return View("WordError", _addWordErrorViewModel);
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
            wordViewModel.TimesCorrectForLearning = SettingsHelper.CORRECT_NUMBER_FOR_LEARNING;
        }
    }
}