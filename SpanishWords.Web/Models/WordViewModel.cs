using Microsoft.AspNetCore.Mvc.Rendering;
using SpanishWords.Models;

namespace SpanishWords.Web.Models
{
    public class WordViewModel
    {
  
        public List<Word> Words = new List<Word>();

        public Word Word { get; set; } = new Word();

        public IEnumerable<SelectListItem> GrammaticalGenders { get; set; }

        public IEnumerable<SelectListItem> LexicalCategories { get; set; }
    }
}