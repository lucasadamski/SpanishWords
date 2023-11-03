using EFDataAccess.DataAccess;
using Microsoft.AspNetCore.Mvc;
using SpanishWords.Models;

namespace SpanishWords.Web.Controllers
{
    public class AddWordController : Controller
    {
        private WordsContext _wordsContext;
        public AddWordController(WordsContext wordsContext)
        {
            _wordsContext = wordsContext;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Add()
        {
            WordViewModel wordViewModel = new WordViewModel();
            wordViewModel.Word.Spanish = "";
            wordViewModel.Word.English = "";
           
            return View(wordViewModel);
        }

        [HttpPost]
        public IActionResult Add(WordViewModel wordViewModel)
        {
            wordViewModel.Word.LexicalCategoryId = 1;
            wordViewModel.Word.GrammaticalGenderId = 1;
            wordViewModel.Word.UserId = 1;
            wordViewModel.Word.StatisticId = 7;

            _wordsContext.Words.Add(wordViewModel.Word);
            _wordsContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
