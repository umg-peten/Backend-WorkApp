using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Models
{
    public class Response
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
