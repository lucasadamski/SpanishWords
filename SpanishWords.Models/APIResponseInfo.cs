using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishWords.Models
{
    public class APIResponseInfo
    {
        public bool Success { get; set; }
        public bool Error { get; set; }
        public string Data { get; set; } = "";

        public APIResponseInfo()
        {
            Data = "OK";
            Success = true;
            Error = false;
        }

        public void SetAllFields(string data, bool success = true, bool error = false)
        {
            Success = success;
            Error = error;
            Data = data;
        }
    }
}
