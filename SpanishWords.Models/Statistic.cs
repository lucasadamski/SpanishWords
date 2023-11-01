using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpanishWords.Models
{
    public class Statistic
    {
        public int Id { get; set; }
        [Required]
        public int TimesCorrect { get; set; }
        [Required]
        public int TimesIncorrect { get; set; }
        [Required]
        public int TimesTrained { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
        public DateTime? DeleteTime { get; set; }

        /***********************
        *       Realtions     *
        * *********************/
        public Word Word { get; set; }



    }
}
