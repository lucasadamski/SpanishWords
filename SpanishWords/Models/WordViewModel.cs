using SpanishWords.Models;

namespace SpanishWords.Models
{
    public class WordViewModel
    {
  
        public List<Word> Words = new List<Word>();

        public Word Word { get; set; } = new Word();

    }
}