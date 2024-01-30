using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishWords.Models
{
    public class WordDTO
    {
        public string Spanish { get; set; }
        public string English { get; set; }
        public int LexicalCategoryId { get; set; }
        public int? GrammaticalGenderId { get; set; }
        public StatisticDTO? Statistic { get; set; }
    }
}
