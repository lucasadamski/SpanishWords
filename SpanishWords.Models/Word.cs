using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpanishWords.Models
{
    public class Word
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Spanish { get; set; }
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string English { get; set; }
        [Required]
        public int LexicalCategoryId { get; set; }

        public int? GrammaticalGenderId { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public int StatisticId { get; set; }

        /***********************
         *       Realtions     *
         * *********************/


        public Statistic Statistic { get; set; }
        public GrammaticalGender GrammaticalGender { get; set; }
        public LexicalCategory LexicalCategory { get; set; }






    }
}
