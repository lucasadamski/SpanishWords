using EFDataAccess.DataAccess;
using EFDataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using SpanishWords.Models;

namespace SpanishWords.Controllers
{
    public class WordController : Controller
    {
        public IActionResult Index()
        {
            WordViewModel sampleWord = new WordViewModel();

            Word dbSampleWord = ReadWords().FirstOrDefault();

            sampleWord.English = dbSampleWord.English;
            sampleWord.Spanish = dbSampleWord.Spanish;
            sampleWord.LexicalId = dbSampleWord.LexicalId;
            sampleWord.GenderId = dbSampleWord.GenderId;
            sampleWord.UserId = dbSampleWord.UserId;


            return View(sampleWord);
        }

        private IEnumerable<Word> ReadWords()
        {
            using (var db = new WordsContext())
            {
                var records = db.Words.ToList();
                return records;
            }
        }
    }
}
