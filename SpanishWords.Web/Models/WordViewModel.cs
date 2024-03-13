using Microsoft.AspNetCore.Mvc.Rendering;
using SpanishWords.Models.Tables;
using SpanishWords.Models.Views;

namespace SpanishWords.Web.Models
{
    public class WordViewModel
    {
  
        public List<Word> Words = new List<Word>();
        public List<v_Words_Stats> WordsView = new List<v_Words_Stats>();

        public Word Word { get; set; } = new Word();

        public IEnumerable<SelectListItem> GrammaticalGenders { get; set; }

        public IEnumerable<SelectListItem> LexicalCategories { get; set; }

        public int TimesCorrectForLearning { get; set; }

        public List<int> TimesCorrect = new List<int>();
        public List<int> TimesIncorrect = new List<int>();
        public List<int> TimesTrained = new List<int>();
    }
}