using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpanishWords.Models.Tables
{
    public class LexicalCategory
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string Name { get; set; }

        /***********************
        *       Realtions     *
        * *********************/

        public ICollection<Word> Words { get; set; }
    }
}
