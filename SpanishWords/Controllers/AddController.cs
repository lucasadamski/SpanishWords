using EFDataAccess.DataAccess;
using Microsoft.AspNetCore.Mvc;
using SpanishWords.Models;

namespace SpanishWords.Web.Controllers
{
    public class AddController : Controller
    {
        WordsContext _wordsContext;
        public AddController(WordsContext wordsContext)
        {
            _wordsContext = wordsContext;
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
            wordViewModel.Word.StatisticId = 6;

            _wordsContext.Add(wordViewModel.Word);
            _wordsContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            WordViewModel word = new WordViewModel();
            word.Word = _wordsContext.Words.Where(a => a.Id == id).FirstOrDefault();
            return View("Add", word);
        }

        [HttpPost]
        public IActionResult Edit(WordViewModel model)
        {
            model.Word.GrammaticalGenderId = 1;
            model.Word.LexicalCategoryId = 1;
            model.Word.UserId = 1;
            model.Word.StatisticId = 6;

            _wordsContext.Words.Update(model.Word);
            _wordsContext.SaveChanges();

            return RedirectToAction("Index", "Word");
        }

        public IActionResult Delete(int id)
        {
            Word word = _wordsContext.Words.Where(a => a.Id == id).FirstOrDefault();
            _wordsContext.Words.Remove(word);
            _wordsContext.SaveChanges();
            return RedirectToAction("Index", "Word");
        }
    }
}
