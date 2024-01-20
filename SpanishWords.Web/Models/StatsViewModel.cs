using SpanishWords.Models;

namespace SpanishWords.Web.Models
{
    public class StatsViewModel
    {
        public List<Word> Words = new List<Word>();
        public int TotalWords { get; set; }
        public int LearntWords { get; set; }
        public int NotLearntWords { get; set; }
        public Word HardestWordToLearn { get; set; }
        public string AverageTimeForLearningPerWord { get; set; }
        public double AverageQuestionCountPerWord { get; set; }
    }
}
