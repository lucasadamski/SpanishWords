using EFDataAccess.DataAccess;
using SpanishWords.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpanishWords.Web.Models;
using EFDataAccess.Repositories.Infrastructure;

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
            WordViewModel word = new();
            return View(word);
        }

        [HttpPost]
        public IActionResult Add(WordViewModel wordViewModel)
        {
            wordViewModel.Word.GrammaticalGenderId = 1;
            wordViewModel.Word.LexicalCategoryId = 1;
            wordViewModel.Word.UserId = 1;
            wordViewModel.Word.StatisticId = _wordRepository.CreateAndAddStatistic().Id;
            _wordRepository.Add(wordViewModel.Word);

            return RedirectToAction("Add");
        }

        public IActionResult Edit(int id)
        {
            WordViewModel word = new WordViewModel();
            word.Word = _wordRepository.GetWordById(id);
            return View("Add", word);
        }

        [HttpPost]
        public IActionResult Edit(WordViewModel model)
        {
            model.Word.GrammaticalGenderId = 1;
            model.Word.LexicalCategoryId = 1;
            model.Word.UserId = 1;
      

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