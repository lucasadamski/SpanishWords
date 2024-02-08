using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishWords.Models.Tables
{
    public class StudyEntry
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public DateTime? AnswerDuration { get; set; }
        [Required]
        public bool Correct { get; set; }
        public AnswerType AnswerType { get; set; }
        public HelperType HelperType { get; set; }
        public Statistic Statistic { get; set; }
    }
}
