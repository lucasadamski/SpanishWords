using SpanishWords.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishWords.Models.DTOs
{
    public class CreateStatisticDTO
    {
        public Statistic Statistic { get; set; }
        public bool Success { get; set; }

    }
}
