using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Models
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
