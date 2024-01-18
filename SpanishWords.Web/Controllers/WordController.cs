using EFDataAccess.DataAccess;
using SpanishWords.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpanishWords.Web.Models;
using EFDataAccess.Repositories.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SpanishWords.Web.Controllers
{
    [Authorize]
    public class WordController : Controller
    {
        
        private IWordRepository _wordRepository;
        private const int CORRECT_ANSWERS_TO_LEARN = 3;

        public WordController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public IActionResult Index()
        {
           WordViewModel wordViewModel = new WordViewModel();


            wordViewModel.Words = _wordRepository.GetAllWords(User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList(); //string z userId

            for (int i = 0; i < wordViewModel.Words.Count(); i++)
            {
                wordViewModel.TimesCorrect.Add(_wordRepository.GetWordsTimesCorrect(wordViewModel.Words.ElementAt(i).Id));
                wordViewModel.TimesIncorrect.Add(_wordRepository.GetWordsTimesIncorrect(wordViewModel.Words.ElementAt(i).Id));
                wordViewModel.TimesTrained.Add(wordViewModel.TimesCorrect.Last() + wordViewModel.TimesIncorrect.Last() );
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
            
            if (wordViewModel.Word.Spanish == null || wordViewModel.Word.Spanish.Trim() == "") wordViewModel.Word.Spanish = "<Empty>";
            if (wordViewModel.Word.English == null || wordViewModel.Word.English.Trim() == "") wordViewModel.Word.English = "<Empty>";
            wordViewModel.Word.Spanish = wordViewModel.Word.Spanish.Trim();
            wordViewModel.Word.English = wordViewModel.Word.English.Trim();

            wordViewModel.Word.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);  //przypisanie id z Identity
            wordViewModel.Word.StatisticId = _wordRepository.CreateAndAddStatistic(CORRECT_ANSWERS_TO_LEARN).Id;
            _wordRepository.Add(wordViewModel.Word);

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

            _wordRepository.Edit(model.Word);

            return RedirectToAction("Index", "Word");
        }

        public IActionResult Delete(int id)
        {
            Word? word = _wordRepository.GetWordById(id);
            if (word == null)
            {
                /*TODO dać okno z ostrzeżeniem że nie można usunąć danego wyrazu (JavaScript?)*/
                return RedirectToAction("Index", "Word");
            }
             
            _wordRepository.Delete(word);
            return RedirectToAction("Index", "Word");
        }

        
        public IActionResult RestartProgressForAllWords()
        {
            _wordRepository.RestartProgressForAll(User.FindFirstValue(ClaimTypes.NameIdentifier));
            WordViewModel wordViewModel = new WordViewModel();
            LoadAllWordsFromRepository(wordViewModel);
            return View("Index", wordViewModel);
        }

        public IActionResult RestartProgressForOneWord(int id)
        {
            _wordRepository.RestartProgress(id);
            WordViewModel wordViewModel = new WordViewModel();
            LoadAllWordsFromRepository(wordViewModel);
            //return View("Index", wordViewModel);
            return RedirectToAction("Index", "Word");
        }

        private void LoadAllWordsFromRepository(WordViewModel wordViewModel)
        {
            wordViewModel.Words = _wordRepository.GetAllWords(User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList(); //string z userId
            for (int i = 0; i < wordViewModel.Words.Count(); i++)
            {
                int id = wordViewModel.Words.ElementAt(i).Id;
                wordViewModel.TimesCorrect.Add(_wordRepository.GetWordsTimesCorrect(id));
                wordViewModel.TimesIncorrect.Add(_wordRepository.GetWordsTimesIncorrect(id));
                wordViewModel.TimesTrained.Add(_wordRepository.GetWordsTotalTrainedTimes(id));
            }
            wordViewModel.TimesCorrectForLearning = CORRECT_ANSWERS_TO_LEARN;
        }
    }
       
}