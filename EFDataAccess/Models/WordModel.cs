using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Models
{
    public class WordModel
    {
        public int Id { get; set; }
        public string Spanish { get; set; }
        public string English { get; set; }
        public int LexicalId { get; set; }
        public int GenderId { get; set; }
        public int UserId { get; set; }
    }
}
