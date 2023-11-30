using EFDataAccess.DataAccess;
using SpanishWords.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpanishWords.Web.Models;
using EFDataAccess.Repositories.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SpanishWords.Web.Controllers
{
    public class WordController : Controller
    {
        
        private IWordRepository _wordRepository;

        public WordController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public IActionResult Index()
        {


           WordViewModel wordViewModel = new WordViewModel();


            wordViewModel.Words = ReadWordsFromDb();

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

            wordViewModel.Word.UserId = 1; // TODO: dalej na stałe przypisane gdyż tym się zajmie identity/logowanie?
            wordViewModel.Word.StatisticId = _wordRepository.CreateAndAddStatistic().Id;
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
            
            model.Word.UserId = 1; // TODO: użytkownik taki jaki jest obecnie

            _wordRepository.Edit(model.Word);

            return RedirectToAction("Index", "Word");
        }

        public IActionResult Delete(int id)
        {
            Word word = _wordRepository.GetWordById(id);
            _wordRepository.Delete(word);
            return RedirectToAction("Index", "Word");
        }


        private List<Word> ReadWordsFromDb() => _wordRepository.GetAllWords().ToList();
    
    }
}