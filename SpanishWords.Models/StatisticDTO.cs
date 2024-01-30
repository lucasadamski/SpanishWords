using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishWords.Models
{
    public class StatisticDTO
    {
        public DateTime CreateDate { get; set; }  
        public DateTime LastUpdated { get; set; }
        public DateTime? DeleteTime { get; set; }
        public int CorrectAnswersToLearn { get; set; }
    }
}
