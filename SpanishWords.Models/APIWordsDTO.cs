using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishWords.Models
{
    public class APIWordsDTO
    {
        public APIResponseInfo APIResponse { get; set; }
        public List<WordDTO> Words { get; set; }

        public APIWordsDTO()
        {
            APIResponse = new APIResponseInfo();
            Words = new List<WordDTO>();
        }
    }
}
