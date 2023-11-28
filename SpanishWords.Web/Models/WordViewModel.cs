using SpanishWords.Models;

namespace SpanishWords.Web.Models
{
    public class WordViewModel
    {
  
        public List<Word> Words = new List<Word>();

        public Word Word { get; set; } = new Word();

    }
}