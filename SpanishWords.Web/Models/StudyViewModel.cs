using SpanishWords.Models;

namespace SpanishWords.Web.Models
{
    public class StudyViewModel
    {
        public Word RandomWord { get; set; }
        public string Answer { get; set; }

       

        public List<Word> WordsToAnswer { get; set; }

        public List<int> IndexesOfWordsAnswered { get; set; } = new List<int>();
    }
}
