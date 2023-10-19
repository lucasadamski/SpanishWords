using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Models
{
    public class Statistic
    {
        public int Id { get; set; }
        [Required]
        public int WordId { get; set; }
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

    }
}
