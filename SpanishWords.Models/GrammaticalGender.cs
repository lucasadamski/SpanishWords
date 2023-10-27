using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishWords.Models
{
    public class GrammaticalGender
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(9)]
        [Column(TypeName = "varchar(9)")]
        public string Name { get; set; }
    }
}
