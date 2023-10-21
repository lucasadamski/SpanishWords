namespace SpanishWords.Models
{
    public class WordViewModel
    {
        public int Id { get; set; }
      
        public string Spanish { get; set; }
       
        public string English { get; set; }
       
        public int LexicalId { get; set; }

        public int? GenderId { get; set; }

        public int UserId { get; set; }
    }
}
