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
            wordViewModel.Word.StatisticId = 13;

            _wordsContext.Add(wordViewModel.Word);
            _wordsContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
