using EFDataAccess.DataAccess;
using EFDataAccess.Repositories;
using EFDataAccess.Repositories.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SpanishWords.Models;

namespace SpanishWords.Web.Controllers
{
    public class AddController : Controller
    {
        WordsContext _wordsContext;
        private readonly IWordRepository _wordRepository;

        public AddController(IWordRepository wordRepository)
        {
            this._wordRepository = wordRepository;
        }

        public IActionResult Index()
        {
            return View();
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
            wordViewModel.Word.StatisticId = _wordRepository.GetLastStaticticId() + 1;

            _wordRepository.Add(wordViewModel.Word); 

            return RedirectToAction("Index");
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
            model.Word.StatisticId = 6;

            _wordRepository.Edit(model.Word);

            return RedirectToAction("Index", "Word");
        }

        public IActionResult Delete(int id)
        {
            Word word = _wordRepository.GetWordById(id);
            _wordRepository.Delete(word);
            return RedirectToAction("Index", "Word");
        }
    }
}
