using EFDataAccess.DataAccess;
using SpanishWords.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpanishWords.Web.Models;

namespace SpanishWords.Web.Controllers
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
            
        }


        private List<Word> ReadWordsFromDb()
        {
            var records = _wordsContext.Words
                .Include(gg => gg.GrammaticalGender)
                .Include(lc => lc.LexicalCategory)
                .Include(u  => u.User)
                .Include(s  => s.Statistic)
                .ToList();
            return records;
        }

       

    }
}



/*
 *     var records = _wordsContext.Words.Include(g => g.GrammaticalGender).Include(l => l.LexicalCategory).ToList();
            return records; */