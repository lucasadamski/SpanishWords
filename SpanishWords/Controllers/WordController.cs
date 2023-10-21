using EFDataAccess.DataAccess;
using EFDataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpanishWords.Models;

namespace SpanishWords.Controllers
{
    public class WordController : Controller
    {
        public IActionResult Index()
        {

            WordViewModel wordViewModel = new WordViewModel();
            wordViewModel.Words = ReadWordsFromDb();

            return View(wordViewModel);
        }


        private List<Word> ReadWordsFromDb()
        {
            using (var db = new WordsContext())
            {
                var records = db.Words.ToList();
                return records; 
            }
        }

    }
}
