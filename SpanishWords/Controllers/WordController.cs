using EFDataAccess.DataAccess;
using EFDataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpanishWords.Models;

namespace SpanishWords.Controllers
{
    public class WordController : Controller
    {
        private WordsContext _wordsContext;

        public WordController(WordsContext wordsContext)
        {
            _wordsContext = wordsContext;
        }

        public IActionResult Index()
        {

            WordViewModel wordViewModel = new WordViewModel();
            wordViewModel.Words = ReadWordsFromDb();

            return View(wordViewModel);
            return View();
        }


        private List<Word> ReadWordsFromDb()
        {
           
                var records = _wordsContext.Words.ToList();
                return records; 
            
        }

    }
}
