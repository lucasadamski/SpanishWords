using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SpanishWords.Models.Views
{
    [Keyless]
    public class v_Words_Stats
    {        
        public int WordId { get; set; }
        public string Spanish { get; set; }
        public string English { get; set; }
        public int LexicalCategoryId { get; set; }
        public int GrammaticalGenderId { get; set; }
        public string UserId { get; set; }
        public int? CorrectAnswers { get; set; }
        public int? IncorrectAnswers { get; set; }
        public string GenderName { get; set; }
        public string LexicalName { get; set; }

    }
}
