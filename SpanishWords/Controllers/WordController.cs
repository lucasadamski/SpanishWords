using EFDataAccess.DataAccess;
using SpanishWords.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpanishWords.Models;
using SpanishWords.Web;

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


            //Zaczęłem pracować nad tym kodem i go nie skończyłem, spróbuj wysłać poniższy obiekt do widoku Word Index

            //TestWord testWord = new TestWord() { English = "car", Spanish = "coche", UserName = "Ziom", LexicalCategory = "Noun" };
            //return View(testWord);
            
        }


        private List<Word> ReadWordsFromDb()
        {
           
                var records = _wordsContext.Words.ToList();
                return records; 
            
        }

    }
}
