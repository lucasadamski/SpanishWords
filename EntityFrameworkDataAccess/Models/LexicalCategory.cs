using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Models
{
    public class LexicalCategory
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string Name { get; set; }
    }
}
