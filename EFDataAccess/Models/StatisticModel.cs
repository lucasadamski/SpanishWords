using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccess.Models
{
    public class StatisticModel
    {
        public int Id { get; set; }
        public int WordId { get; set; }
        public int TimesCorrect { get; set; }
        public int TimesIncorrect { get; set; }
        public int TimesTrained { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime DeleteTime { get; set; }

    }
}
